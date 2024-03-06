using WebApplication1.ViewModels.SliderViewModels;

namespace WebApplication1.Services.Interfaces
{
	public interface ISliderService
	{
		Task Crete(CreateSliderVM sliderVM);
	}
}
