using Jsanudo.Ecommerce.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanValero.Daw.Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jsanudo.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EcommerceDb _baseDatosDelContexto;

        public CartController(EcommerceDb context)
        {
            _baseDatosDelContexto = context;
        }

        // GET: devuelve todos los productos del carrito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarrito()
        {
            return await _baseDatosDelContexto.Carrito.Include(carrito => carrito.Product).ToListAsync();
        }

        // GET devuelve un carrito según la ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCarritoById(String email)
        {
            var carrito = await _baseDatosDelContexto.Carrito.FindAsync(email);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        // POST añade un carrito
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCarrito(Cart carrito)
        {
            _baseDatosDelContexto.Carrito.Add(carrito);
            await _baseDatosDelContexto.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = carrito.Id }, carrito);
        }

        // PUT edita un carrito
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCarrito(String email, Cart carrito)
        {
            if (email != carrito.Email) //hacer aqui lo del @hotmail.com
            {
                return BadRequest();
            }

            _baseDatosDelContexto.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _baseDatosDelContexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw e;
            }

            return NoContent();
        }

        // DELETE elimina un carrito según la ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(String email)
        {
            var carrito = await _baseDatosDelContexto.Carrito.FindAsync(email);
            if (carrito == null)
            {
                return NotFound();
            }

            _baseDatosDelContexto.Carrito.Remove(carrito);
            await _baseDatosDelContexto.SaveChangesAsync();

            return NoContent();
        }
    }
}