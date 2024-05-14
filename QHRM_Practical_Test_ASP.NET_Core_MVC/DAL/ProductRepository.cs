using Dapper;
using Microsoft.Data.SqlClient;
using QHRM_Practical_Test_App.Models;
using System.Data;

namespace QHRM_Practical_Test_App.DAL
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Product>("SELECT * FROM Products");
        }

        public Product GetProductById(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }

        public void AddProduct(Product product)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            string sql = "INSERT INTO Products (Name, Description, Created) VALUES (@Name, @Description, @Created)";
            db.Execute(sql, product);
        }

        public void UpdateProduct(Product product)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            string sql = "UPDATE Products SET Name = @Name, Description=@Description, Created = @Created WHERE Id = @Id";
            db.Execute(sql, product);
        }

        public void DeleteProduct(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            string sql = "DELETE FROM Products WHERE Id = @Id";
            db.Execute(sql, new { Id = id });
        }
    }
}
