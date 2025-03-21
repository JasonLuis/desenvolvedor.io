using ApiFuncional.Data;
using ApiFuncional.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ApiFuncional.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController: ControllerBase
{

    private readonly ApiDbContext _context;

    public ProdutosController(ApiDbContext context)
    {
        _context = context; // injeção de dependência
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<Produto>>>  GetProdutos()
    {
        if (_context.Produtos == null) return NotFound();

        return await _context.Produtos.ToListAsync(); // pode usar o Ok(await _context.Produtos.ToListAsync())
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        if (_context.Produtos == null) return NotFound();

        var produto = await _context.Produtos.FindAsync(id);

        if(produto == null) return NotFound();

        return produto; // pode usar o Ok(await _context.Produtos.FindAsync(id))
    }
         

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {

        if (_context.Produtos == null) return Problem("Erro ao criar um produto, contate o suporte!");

        if (!ModelState.IsValid)
        {
            // return BadRequest(ModelState); // retorna os erros de validação
            // return ValidationProblem(ModelState); // retorna os erros de validação mais detailhado
            return ValidationProblem(new ValidationProblemDetails(ModelState) 
            {  
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync(); // comita as alterações no banco

        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> PutProduto(int id, Produto produto)
    {
        if(id != produto.Id) return BadRequest();

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

         // _context.Produtos.Update(produto); -> para realizar o update
        // _context.Entry(produto).State = EntityState.Modified; -> posso usar dessa maneira também


        /*
         * Se um objeto ja foi manipulado em memoria, pode ser que ocorra um erro de concorrencia.
         * Por exemplo, se um usuario alterar um produto e outra pessoa alterar esse produto ao mesmo tempo.
         * Para tratar o problema, realizamos o processo abaixo para precaver esse erro.
         */

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoExists(id))
            {
                return NotFound();
            }

            throw;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> DeleteProduto(int id)
    {

        if (_context.Produtos == null) return NotFound();

        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool ProdutoExists(int id)
    {
        return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
