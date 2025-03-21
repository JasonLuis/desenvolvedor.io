using Microsoft.AspNetCore.Mvc;

namespace PrimeiraApi.Controllers;

[ApiController]
[Route("api/demo")]
public class TesteController: ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)] // Produzir um tipo de resposta, que devolve um Produto e o status code de 200
    public IActionResult Get()
    {
        return Ok(new Produto { Id = 1, Nome = "Notebook" });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)] // Se a requisição for bem sucedida, devolve um Produto e o status code de 200
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Se a requisição não for bem sucedida, devolve o status code de 404
    public IActionResult Get(int id)
    {
        return Ok(new Produto { Id = 1, Nome = "Notebook" });
    }

    [HttpPost()]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)] // Se a requisição for bem sucedida, cria um Produto e devolve um Produto e o status code de 201
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Se a requisição não for bem sucedida, devolve o status code de 400
    public IActionResult Post(Produto produto)
    {
        // CreatedAtAction - cria uma resposta com o status code 201
        // No metedo CreatedAtAction, recebe o nome do metodo que vai ser executado e o parametro que vai ser passado
        return CreatedAtAction("Get", new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Se a requisição for bem sucedida, atualiza um Produto e devolve o status code de 204
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Se a requisição não for bem sucedida, devolve o status code de 400
    public IActionResult Put(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();
        
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // Se a requisição for bem sucedida, deleta um Produto e devolve o status code de 204
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Se a requisição não for bem sucedida, devolve o status code de 404
    public IActionResult Delete(int id)
    {
        if (id == 0) return NotFound();

        return NoContent();
    }

}
