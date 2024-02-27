using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using System.Collections.Generic;
using static DSU24_Grupp5.ApiResponseProcess.StatisticsHelper;


namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayAgeViewModel
    {
       public List <int> SortedAges { get; set; } = new();
        public List<string> SelectedDesoCodes { get; set; } = new();
        public DisplayAgeViewModel( IndexFilterViewModel indexFilterViewModel, DesoDto desoDto)
        {
            SelectedDesoCodes = StatisticsHelper.DesoCodes(desoDto.Areas, indexFilterViewModel);

            SortedAges = StatisticsHelper.AgeCounter(indexFilterViewModel, SelectedDesoCodes);

        }
    }
}
