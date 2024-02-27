using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;

namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayDesoAreaViewModel
    {
        public List<AreaDesoDto> DesoAreasDto = new();

        public List<string> SelectedDesoNames { get; private set; }

        public List<string> SelectedDesoCodes { get; private set; }

        public List<int> SelectedDesoRecordsPatiens { get; private set; }

        public List<int> SelectedDesoPopulation {  get; private set; }


        public DisplayDesoAreaViewModel(DesoDto desoDto, IndexFilterViewModel indexFilterViewModel, ScbDto scbDto)
        {
            DesoAreasDto = desoDto.Areas;

            SelectedDesoNames = StatisticsHelper.DesoNames(DesoAreasDto, indexFilterViewModel);

            SelectedDesoCodes = StatisticsHelper.DesoCodes(DesoAreasDto, indexFilterViewModel);

            SelectedDesoRecordsPatiens = StatisticsHelper.GetRecordsPerDeso(SelectedDesoNames, indexFilterViewModel);

            SelectedDesoPopulation = StatisticsHelper.PopulationByDesoCodes(SelectedDesoCodes, scbDto, indexFilterViewModel);
        }

      
    }
}
