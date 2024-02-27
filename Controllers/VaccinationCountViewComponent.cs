using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.DeSO;

namespace DSU24_Grupp5.Controllers
{
    public class VaccinationCountViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            //VaccinationCountDto vaccinationCountDesoDto = ApiHandler.GetVaccinationCountFromJson();

            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();
            var model = new DisplayVaccinationCountViewModel(indexFilterViewModel, desoDto);
            return View(model);
        }
    }
}
