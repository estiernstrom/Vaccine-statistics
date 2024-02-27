using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU24_Grupp5.Controllers
{
    public class VaccinationsPerWeek : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DesoCodeDto desoCodeDto = JsonFileClient.GetAllVaccinationDesoDataFromJson();
            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();
            var model = new DisplayVaccinationsPerWeekViewModel(desoCodeDto, desoDto);
            return View(model);
        }

      
    }
}
