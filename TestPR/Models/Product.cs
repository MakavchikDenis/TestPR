using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPR.Models
{
    public class Product
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:000}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; } = decimal.Zero;


        public string? NoteSpecial { get; set; }

        public string? NoteGeneral { get; set; }

        //[Required]
        //[ForeignKey(nameof(Category))]
        public int categoryId { get; set; }

        public Category category { get; set; }

    }
}
