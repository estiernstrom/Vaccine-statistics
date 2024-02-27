using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using DSU24_Grupp5.Models.Dto.DeSO;
using System.Text.Json;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.Dto.SCB;

namespace DSU24_Grupp5.Controllers
{
    public class DesoAreaViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();
            ScbDto scbDto = JsonFileClient.GetScbAgeAndGenderByDesoFromJson();
            var model = new DisplayDesoAreaViewModel(desoDto, indexFilterViewModel, scbDto);

            return View(model);
        }
    }
}
