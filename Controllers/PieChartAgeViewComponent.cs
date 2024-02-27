using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace DSU24_Grupp5.Controllers
{
    public class PieChartAgeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();

            var model = new DisplayAgeViewModel(indexFilterViewModel, desoDto);
            return View(model);
        }
    }
}
