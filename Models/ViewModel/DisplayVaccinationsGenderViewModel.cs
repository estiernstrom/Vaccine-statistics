using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;

namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayVaccinationsGenderViewModel
    {
        public List<int> PopulationVaccinatedByGender = new ();
        public List<int> PopulationNotVaccinatedByGender = new();
        public List<int> ScbPopulationByGender = new ();
        public List<string> SelectedDesoNames = new ();
        public List<string> SelectedDesoCodes { get; private set; }


        public DisplayVaccinationsGenderViewModel(IndexFilterViewModel indexFilterViewModel, List<AreaDesoDto> DesoAreasDto, ScbDto scbDto)
        {
            SelectedDesoNames = StatisticsHelper.DesoNames(DesoAreasDto, indexFilterViewModel);
            SelectedDesoCodes = StatisticsHelper.DesoCodes(DesoAreasDto, indexFilterViewModel);
            PopulationVaccinatedByGender = StatisticsHelper.GenderCounter(indexFilterViewModel, SelectedDesoNames);
            ScbPopulationByGender = StatisticsHelper.GenderTotalCountScb(indexFilterViewModel, SelectedDesoCodes, scbDto); 
            PopulationNotVaccinatedByGender =  StatisticsHelper.GenderNotVaccinated(PopulationVaccinatedByGender, ScbPopulationByGender);
        }
    }
}
