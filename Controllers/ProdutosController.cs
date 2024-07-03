using apiFornecedor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace apiFornecedor.Controllers
{
    [ApiController]

    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ProdutosController(ApiDbContext context)
        {

            _context = context;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            if (_context.Produtos == null)
            {
                return NotFound();
            }
            return await _context.Produtos.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Produto>> GetProdutosById(int id)
        {

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            return produto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Produto>> InserirProduto(Produto produto)
        {
            if (_context.Produtos == null)
            {
                return Problem("erro a o criar o produto, contacte o suporte");
            }
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Produto>> AtualizarProduto(int id, Produto produto)
        {
            if (id != produto.Id) return BadRequest();
            _context.Entry(produto).State = EntityState.Modified;

            if (!ModelState.IsValid) return ValidationProblem(ModelState);

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
                else{
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Produto>> DeletarProduto(int id)
        {
            if(_context.Produtos == null) { return NotFound(); }
            var produto = await _context.Produtos.FindAsync(id);
            if(produto == null) return NotFound();
            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos.Any(p => p.Id == id));
        }

    }
}
