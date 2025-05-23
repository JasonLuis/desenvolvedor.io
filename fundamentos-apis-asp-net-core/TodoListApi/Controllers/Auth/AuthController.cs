using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoListApi.Models;

namespace TodoListApi.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{

    private readonly SignInManager<User> _signInManager; // gerenciador de autenticação
    private readonly UserManager<User> _userManager; // gerenciador de usuários
    private readonly JwtSettings _jwtSettings; // configurações do JWT

    public AuthController(SignInManager<User> signInManager,
                          UserManager<User> userManager,
                          IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Registrar(RegisterDto registerUser)
    {
        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var user = new User
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password); // cria o usuário

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false); // faz o login do usuário
            return Ok(await GerarToken(user.Email));
        }
        return Problem("Erro ao registrar o usuário");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginUser)
    {
        if (!ModelState.IsValid) // verifica se o modelo é válido
            return ValidationProblem(ModelState); // retorna os erros de validação

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true); // faz o login do usuário

        if (result.Succeeded) return Ok(await GerarToken(loginUser.Email));
        
        return Problem("Usuário ou senha inválidos.");
    }

    private async Task<string> GerarToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email); // busca o usuário pelo email
        var roles = await _userManager.GetRolesAsync(user); // busca as roles do usuário

        var claims = new List<Claim> // cria as claims do usuário
        {
            new Claim(ClaimTypes.Name, user.UserName!), // nome do usuário
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // id do usuário
        };

        foreach (var role in roles) // percorre as roles 
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); // adiciona a role do usuario
        }

        var tokenHandler = new JwtSecurityTokenHandler(); // cria o manipulador de token JWT
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret!); // pega a chave secreta do JWT

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // define as claims do usuário
            Issuer = _jwtSettings.Issuer, // define o emissor do token
            Audience = _jwtSettings.Audience, // define o audiencia do token
            Expires = DateTime.UtcNow.AddHours(2), // define o tempo de expiração do token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // define as credenciais de assinatura do token
        });

        var encondedToken = tokenHandler.WriteToken(token); // convete o token em string

        return encondedToken; // retorna o token
    }
}
