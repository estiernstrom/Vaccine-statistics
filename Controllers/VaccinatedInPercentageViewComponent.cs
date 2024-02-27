using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU24_Grupp5.Controllers
{
    public class VaccinationsInPercentageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();
            ScbDto scbDto = JsonFileClient.GetScbAgeAndGenderByDesoFromJson();
            var model = new DisplayVaccinationsInPercentageViewModel(indexFilterViewModel, scbDto, desoDto);
            return View(model);
        }
    }
}
