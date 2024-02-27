using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU24_Grupp5.Controllers
{
    public class CompareDesosDeso1ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            ScbDto scbDto = JsonFileClient.GetScbAgeAndGenderByDesoFromJson();

            var model = new DisplayCompareDesosArea1ViewModel(indexFilterViewModel, scbDto);
            return View(model); 
        }
    }
}
