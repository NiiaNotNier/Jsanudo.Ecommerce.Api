using System.Text.Json.Serialization;

namespace SanValero.Daw.Ecommerce.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public string ProductName => Product?.Name;
        //public decimal Price => Product.Price;
        //public decimal Total => Product.Price * Quantity;
    }
}