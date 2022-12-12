namespace DivyaGyan_WebApp_MVC.Models
{
    public class Product
    {
       public Product() { }
       public Product(int id, string? name, string? code, string? description, DateTime createdDateTime)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            CreatedDateTime = createdDateTime;
        }

        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Code { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
