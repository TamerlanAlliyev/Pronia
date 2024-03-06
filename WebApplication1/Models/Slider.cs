using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the Url")]
        public string ImgUrl { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required,MaxLength(150)]
        public string Description { get; set; }
        [Required,MaxLength(50)]
        public string? Offer { get; set; }
        [Required,MaxLength(50)]
        public string ButtonText { get; set; }
    }
}
