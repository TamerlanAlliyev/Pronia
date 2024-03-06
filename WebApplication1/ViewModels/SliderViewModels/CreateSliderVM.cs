using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.SliderViewModels
{
    // record - daxil edlimis datani sonradan deydirmir
    // her slider buradan yaradilmis olacaq
    public record CreateSliderVM
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string Description { get; set; }
        [Required, MaxLength(50)]
        public string? Offer { get; set; }
        [Required, MaxLength(50)]
        public string ButtonText { get; set; }

        // sekil musiqi video doc ve s. kimi fayllari yuklemek ucun "IFormFile"-dan istifade olunur
        public IFormFile ImageFile { get; set; }
    }
}
