using Microsoft.AspNetCore.Mvc;

namespace PrimeiraApi.Controllers
{
    [ApiController] // indica que essa classe é um controller
    // [Route("[controller]")]  rota tem o mesmo nome da classe `[controller]`
    [Route("api/minha-controller")]
    public class WeatherForecastController : ControllerBase // toda classe controller herda de controllerBase
    {

        // caso queira injetar dependencias, utilize o construtor
        public WeatherForecastController()
        {
        }

        [HttpGet] // indica que esse método é um GET
        [Route("get")]
        public IActionResult Get()
       {
            return Ok();
       }

        // Rota ambígua é quando tem mais de um método com a mesma rota
        
        // Essa rota seria ambígua caso não recebesse o id como parametro
        [HttpGet("get/{id}")] // indica que esse método é um GET
        public IActionResult Get2(int id)
        {
            return Ok();
        }

        /*
         * [HttpGet("get/{id}/{nome}")] - rota que recebe dois parametros 
         * seu passar no id uma string e no nome uma int, a rota receberia um erro de validação
         * 
         * [HttpGet("get/{id:int}/{nome:string}")] - rota que recebe dois parametros, mas nesse caso forço o usuario a passar um int para o id e uma string para o nome
         */
        [HttpGet("get/{id:int}/{nome}")]
        public IActionResult Get3(int id, string nome)
        {
            return Ok();
        }
    }
}

// documentação microsoft fundamentals routing => https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/routing?view=aspnetcore-9.0
