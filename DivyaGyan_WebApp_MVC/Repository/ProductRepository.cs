using DivyaGyan_WebApp_MVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace DivyaGyan_WebApp_MVC.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
        public void CreateProduct(Product product ) {
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
    }
}
