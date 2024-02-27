using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU24_Grupp5.Controllers
{
    public class FilterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IndexFilterViewModel indexFilterViewModel)
        {
            DesoDto desoDto = JsonFileClient.GetDesoInfoFromJson();
            var model = new DisplayFilterViewModel(desoDto, indexFilterViewModel);
            return View(model);
        }
    }
}
