using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using System.Collections.Generic;
using static DSU24_Grupp5.ApiResponseProcess.StatisticsHelper;


namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayVaccinationsInPercentageViewModel
    {
        public double VaccinationsInPercentage { get; set; }
        public List<string> SelectedDesoCodes { get; set; }
        public int AmountOfVaccinated { get; set; }
        public int TotalCountScb {  get; set; }
        public DisplayVaccinationsInPercentageViewModel(IndexFilterViewModel indexFilterViewModel, ScbDto scbDto, DesoDto desoDto)
        {
            SelectedDesoCodes = StatisticsHelper.DesoCodes(desoDto.Areas, indexFilterViewModel);
            AmountOfVaccinated = StatisticsHelper.TotalAmountOfVaccinated(SelectedDesoCodes, indexFilterViewModel); 
            TotalCountScb = StatisticsHelper.TotalPopulationCountScb(SelectedDesoCodes, scbDto);
            VaccinationsInPercentage = TotalAmountOfVaccinatedPercentage(TotalCountScb, AmountOfVaccinated);

        }
    }
}
