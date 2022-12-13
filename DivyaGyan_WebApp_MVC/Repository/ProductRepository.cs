using DivyaGyan_WebApp_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace DivyaGyan_WebApp_MVC.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;Connect Timeout=30";
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            string query = "Select * from Product;";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.CommandType = CommandType.Text;
            sqlConnection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product(reader.GetInt32("Id"), reader.GetString("Name"),
                    reader.GetString("Code"), reader.GetString("Description"), reader.GetDateTime("CreatedDateTime"));
                products.Add(product);
            }
            sqlConnection.Close();
            return products;
        }
        public void CreateProduct(Product product)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            var cmd = "Insert into Product values (@Name, @Code, @Desc, @Create dDateTime)";
            SqlCommand sqlCommand = new(cmd, conn);
            sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = product.Name;
            sqlCommand.Parameters.Add("@Code", SqlDbType.VarChar).Value = product.Code;
            sqlCommand.Parameters.Add("@Desc", SqlDbType.VarChar).Value = product.Description;
            sqlCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = product.CreatedDateTime;
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }

        public Product GetProductById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            string query = "Select * from Product where id=@id";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            sqlConnection.Open();
            var reader = cmd.ExecuteReader();
            Product product = new Product();
            while (reader.Read())
            {
                product.Id = reader.GetInt32("Id");
                product.Name = reader.GetString("Name");
                product.Code = reader.GetString("Code");
                product.Description = reader.GetString("Description");
                product.CreatedDateTime = reader.GetDateTime("CreatedDateTime");
            }
            sqlConnection.Close();
            return product;
        }
        public void UpdateProduct(Product product)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            string query = "Update product set Name=@name, Code=@code, Description=@desc, CreatedDateTime=@createdDateTime" +
                " where Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = product.Name;
            sqlCommand.Parameters.Add("@code", SqlDbType.VarChar).Value = product.Code;
            sqlCommand.Parameters.Add("@desc", SqlDbType.VarChar).Value = product.Description;
            sqlCommand.Parameters.Add("@createdDateTime", SqlDbType.DateTime).Value = product.CreatedDateTime;
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = product.Id;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void DeleteProduct(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            string query = "Delete from product where Id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
