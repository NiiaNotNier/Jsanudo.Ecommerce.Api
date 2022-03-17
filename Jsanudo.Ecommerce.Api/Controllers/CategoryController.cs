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
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceDb _baseDatosDelContexto;

        public CategoryController(EcommerceDb contextdb)
        {
            _baseDatosDelContexto = contextdb;
        }

        // GET: devuelve todas las categorías
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            return await _baseDatosDelContexto.Categoria.ToListAsync();
        }

        // GET devuelve una categoría según la ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _baseDatosDelContexto.Categoria.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST permite añadir una categoría
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            _baseDatosDelContexto.Categoria.Add(category);
            await _baseDatosDelContexto.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // PUT edita una categoría
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _baseDatosDelContexto.Entry(category).State = EntityState.Modified;

            try
            {
                await _baseDatosDelContexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // DELETE elimina una categoría según la ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _baseDatosDelContexto.Categoria.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _baseDatosDelContexto.Categoria.Remove(category);
            await _baseDatosDelContexto.SaveChangesAsync();

            return NoContent();
        }

        //comprueba si existe una categoría por la ID
        private bool CategoryExists(int id)
        {
            return _baseDatosDelContexto.Categoria.Any(categoria => categoria.Id == id);
        }
    }
}