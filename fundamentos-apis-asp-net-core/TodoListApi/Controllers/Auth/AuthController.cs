using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
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

    [HttpGet("login-google")]
    public IActionResult LoginComGmail()
    {
        var props = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
        return Challenge(props, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
            return BadRequest("Erro ao autenticar com Google");

        var email = result.Principal.FindFirst(ClaimTypes.Email)!.Value;
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && user.GoogleId == null)
            return BadRequest("Erro, usuário já possui uma conta cadastrada nessa plataforma.");

        if (user == null)
        {
            var createUser = new User
            {
                UserName = result.Principal.Identity!.Name!.Split(' ')[0],
                Email = result.Principal.FindFirst(ClaimTypes.Email)!.Value,
                EmailConfirmed = true,
                GoogleId = result.Principal.FindFirst(ClaimTypes.NameIdentifier)!.Value
            };

            var createResult = await _userManager.CreateAsync(createUser);

            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(createUser, false);
                return Ok(await GerarToken(createUser!.Email));
            }

            return Problem("Erro ao registrar o usuário");
        }

        return Ok(await GerarToken(email));
    }

    private async Task<AuthTokenResponse> GerarToken(string email)
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

        return new AuthTokenResponse
        {
            Token = encondedToken, // retorna o token
            Expiration = token.ValidTo // retorna a data de expiração do token
        };
    }

    public class AuthTokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
