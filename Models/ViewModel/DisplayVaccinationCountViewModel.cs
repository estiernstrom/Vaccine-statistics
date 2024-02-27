using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
namespace DSU24_Grupp5.Models.ViewModel

{
    public class DisplayVaccinationCountViewModel
    {
       
        public int AmountOfVaccinated { get; set; }
        public List<string> SelectedDesoCodes { get; set; }

        public DisplayVaccinationCountViewModel(IndexFilterViewModel indexFilterViewModel, DesoDto desoDto)
        {
            SelectedDesoCodes = StatisticsHelper.DesoCodes(desoDto.Areas, indexFilterViewModel);
            AmountOfVaccinated = StatisticsHelper.TotalAmountOfVaccinated(SelectedDesoCodes, indexFilterViewModel);
        }
    }
}
