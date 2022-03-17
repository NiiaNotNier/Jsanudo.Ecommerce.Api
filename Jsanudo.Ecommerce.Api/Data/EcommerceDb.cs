using Microsoft.EntityFrameworkCore;
using SanValero.Daw.Ecommerce.Entities;

namespace Jsanudo.Ecommerce.Api.Data
{
    public class EcommerceDb : DbContext
    {
        public DbSet<Cart> Carrito { get; set; }
        public DbSet<Category> Categoria { get; set; }
        public DbSet<Product> Producto { get; set; }

        public EcommerceDb(DbContextOptions<EcommerceDb> options) : base(options)
        {
        }
    }
}