using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;

namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayCompareDesosArea2ViewModel
    {
        public List<string>? DesoCode { get; set; }
        public List<string>? DesoName { get; set; } = new();
        public int RecordPatients { get; private set; }
        public int Population { get; set; }
        public List<int> SortedAges { get; private set; }
        public List<int> PopulationVaccinatedByGender { get; private set; }
        public List<int> ScbPopulationByGender { get; private set; }
        public List<int> PopulationNotVaccinatedByGender { get; private set; }
        public double VaccinationsInPercentage { get; private set; } = 0;
        public DisplayCompareDesosArea2ViewModel(IndexFilterViewModel indexFilterViewModel, ScbDto scbDto)
        {
            DesoCode = [indexFilterViewModel.DesoCode2];
            string? desoName = StatisticsHelper.DesoNameByDesoCode(DesoCode[0]);
            DesoName.Add(desoName ?? "");
            RecordPatients= StatisticsHelper.TotalAmountOfVaccinated(DesoCode, indexFilterViewModel);
            Population = StatisticsHelper.PopulationByDesoCodes(DesoCode, scbDto, indexFilterViewModel)[0];
            SortedAges = StatisticsHelper.AgeCounter(indexFilterViewModel, DesoCode);
            PopulationVaccinatedByGender = StatisticsHelper.GenderCounter(indexFilterViewModel, DesoName);
            ScbPopulationByGender = StatisticsHelper.GenderTotalCountScb(indexFilterViewModel, DesoCode, scbDto);
            PopulationNotVaccinatedByGender = StatisticsHelper.GenderNotVaccinated(PopulationVaccinatedByGender, ScbPopulationByGender);
            VaccinationsInPercentage = StatisticsHelper.TotalAmountOfVaccinatedPercentage(Population, RecordPatients);
        }
    }
}
