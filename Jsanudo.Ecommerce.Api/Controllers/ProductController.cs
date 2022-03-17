using Jsanudo.Ecommerce.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanValero.Daw.Ecommerce.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jsanudo.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceDb _baseDatosDelContexto;

        public ProductController(EcommerceDb contextdb)
        {
            _baseDatosDelContexto = contextdb;
        }

        // GET: devuelve todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            return await _baseDatosDelContexto.Producto.ToListAsync();
        }

        // GET devuelve un producto según la ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _baseDatosDelContexto.Producto.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST añade un producto
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _baseDatosDelContexto.Producto.Add(product);
            await _baseDatosDelContexto.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT edita un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _baseDatosDelContexto.Entry(product).State = EntityState.Modified;
            try
            {
                await _baseDatosDelContexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE elimina un producto de la BD
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _baseDatosDelContexto.Producto.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _baseDatosDelContexto.Producto.Remove(product);
            await _baseDatosDelContexto.SaveChangesAsync();

            return NoContent();
        }

        //comprueba si existe un producto por la ID
        private bool ProductExists(int id)
        {
            return _baseDatosDelContexto.Producto.Any(item => item.Id == id);
        }
    }
}