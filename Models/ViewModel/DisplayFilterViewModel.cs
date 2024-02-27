using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto.Batches;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using System.Reflection;

namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayFilterViewModel
    {
        public int MostAmountOfDoses { get; private set; }
        public BatchesDto ListOfBatchNumbers {  get; private set; } 
        public List<string>? ListOfVaccineNames { get; private set; }
        public List<string>? ListOfManufacturers {  get; private set; } 
        public List<string> ListOfDesoAreas { get; private set; }
        public List<string>? ListOfVaccStations { get; private set; }
        public DesoDto DesoDto { get; private set; }    

        public int? SelectedDose { get; private set; }
        public string? SelectedBatchNumber { get; private set; }
        public string? SelectedVaccineName { get; private set; }
        public string? SelectedManufacturer { get; private set; }
        public List <string>? SelectedListOfDesoAreas { get; private set; }
        public string? SelectedVaccStation { get; private set; }
        public DesoDto? SelectedDesoDto { get; private set; }
        public string? SelectedGender { get; private set; }
        public string? SelectedAge { get; private set; }

        public string? CompareDesoArea1 { get; private set; }
        public string? CompareDesoArea2 { get; private set; }



        public DisplayFilterViewModel(DesoDto desoDto, IndexFilterViewModel indexFilterViewModel)
        {
            ListOfVaccStations = StatisticsHelper.GetListOfVaccStations();
            DesoDto = StatisticsHelper.GetListOfDesoAreas(desoDto);
            ListOfBatchNumbers = StatisticsHelper.GetBatchNumbers();
            ListOfVaccineNames = StatisticsHelper.GetVaccineNames();
            ListOfManufacturers = StatisticsHelper.GetManufacturers();
            MostAmountOfDoses = StatisticsHelper.CountMostAmountOfVaccinations();

            CompareDesoArea1 = indexFilterViewModel.DesoCode1;
            CompareDesoArea2 = indexFilterViewModel.DesoCode2;
            SelectedDose = indexFilterViewModel.DoseNumber;
            SelectedBatchNumber = indexFilterViewModel.BatchNumber;
            SelectedVaccineName = indexFilterViewModel.VaccineName;
            SelectedManufacturer = indexFilterViewModel.Manufacturer;
            SelectedListOfDesoAreas = indexFilterViewModel.DesoCodes;
            SelectedVaccStation = indexFilterViewModel.VaccinationStation;
            SelectedGender = indexFilterViewModel.Gender;
            SelectedAge = indexFilterViewModel.AgeInterval;
        }

    }
}
