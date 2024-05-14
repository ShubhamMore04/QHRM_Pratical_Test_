using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QHRM_Practical_Test_App_2.Models;
using System.Data;

namespace QHRM_Practical_Test_App_2.DAL
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly IConfiguration _config;

        public ProductDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using IDbConnection conn = Connection;
            string query = "SELECT * FROM Products";
            conn.Open();
            return await conn.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using IDbConnection conn = Connection;
            string query = "SELECT * FROM Products WHERE Id = @Id";
            conn.Open();
            return await conn.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<int> AddProductAsync(Product product)
        {
            using IDbConnection conn = Connection;
            string query = @"INSERT INTO Products (Name, ) 
                             VALUES (@Name, @Description, @Created); 
                             SELECT CAST(SCOPE_IDENTITY() as int)";
            conn.Open();
            return await conn.ExecuteScalarAsync<int>(query, product);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            using IDbConnection conn = Connection;
            string query = @"UPDATE Products SET Name = @Name, Description = @Description, Created = @Created 
                             WHERE Id = @Id";
            conn.Open();
            return await conn.ExecuteAsync(query, product);
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using IDbConnection conn = Connection;
            string query = @"DELETE FROM Products WHERE Id = @Id";
            conn.Open();
            return await conn.ExecuteAsync(query, new { Id = id });
        }

    }
}
