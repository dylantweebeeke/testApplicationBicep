using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using testApplication.Models;

namespace testApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Product> getAll()
        {
            var products = GetProducts();
            return products;
        }

        private IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("ProductsDatabase")))
            {
                var sql = "SELECT product_id, product_name, price FROM products";

                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var product = new Product()
                    {
                        product_id = (int)reader["product_id"],
                        product_name = reader["product_name"].ToString(),
                        price = (int)reader["price"]
                    };

                    products.Add(product);
                }
            }

            return products;
        }
    } 
}
