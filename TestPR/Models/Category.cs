using System.ComponentModel.DataAnnotations;

namespace TestPR.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
