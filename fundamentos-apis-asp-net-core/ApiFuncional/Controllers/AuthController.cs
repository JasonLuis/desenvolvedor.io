using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiFuncional.Controllers;

[ApiController]
[Route("api/conta")]
public class AuthController : ControllerBase
{

    private readonly SignInManager<IdentityUser> _signInManager; // gerenciador de autenticação
    private readonly UserManager<IdentityUser> _userManager; // gerenciador de usuários
    private readonly JwtSettings _jwtSettings; // configurações do JWT

    public AuthController(SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegisterUserViewModel registerUser)
    {
        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password); // cria o usuário

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false); // faz o login do usuário
            return Ok(await GerarJwt(user.Email));
        }
        return Problem("Erro ao registrar o usuário");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserViewModel loginUser)
    {
        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true); // tenta fazer o login, caso falhe 5x o login, trava o usuario por 15 minutos

        if (result.Succeeded) return Ok(await GerarJwt(loginUser.Email)); // se o login for bem sucedido, gera o JWT

        return Problem("UsuárioPPP ou senha inválidos");
    }

    private async Task<string> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email); // busca o usuário pelo email
        var roles = await _userManager.GetRolesAsync(user); // busca as roles do usuário
        
        var claims = new List<Claim> // cria os claims do usuário
        {
            new Claim(ClaimTypes.Name, user.UserName), // nome do usuário

        };

        foreach (var role in roles) // percorre as roles
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); // adiciona a role do usuário
        }

        var tokenHandler = new JwtSecurityTokenHandler(); // cria o manipulador de tokens JWT
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo); // converte a chave secreta para bytes

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // adiciona os claims ao token
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encondedToken = tokenHandler.WriteToken(token); // transforma o token em string

        return encondedToken; // retorna o token
    }
}
