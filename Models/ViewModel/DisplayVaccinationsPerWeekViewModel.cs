using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using Microsoft.AspNetCore.Mvc;

namespace DSU24_Grupp5.Models.ViewModel
{
    public class DisplayVaccinationsPerWeekViewModel
    {
        
        public List<int> vaccinationsPerWeek2020 { get; set; }
        public List<int> vaccinationsPerWeek2021 { get; set; }
        public List<int> vaccinationsPerWeek2022 { get; set; }
        public List<int> vaccinationsPerWeek2023 { get; set; }
        public List<int> vaccinationsPerWeek2024 { get; set; }

        public List<List<int>> vaccinationsPerWeekDeso2020 { get; set; }
        public List<List<int>> vaccinationsPerWeekDeso2021 { get; set; }
        public List<List<int>> vaccinationsPerWeekDeso2022 { get; set; }

        public List<List<int>> vaccinationsPerWeekDeso2023 { get; set; }
        public List<List<int>> vaccinationsPerWeekDeso2024 { get; set; }
       
        public List<string> DesoNames { get; private set; }
        public int MyProperty { get; set; }


        public DisplayVaccinationsPerWeekViewModel(DesoCodeDto desoCodeDto, DesoDto desoDto)
        {
            DesoNames = StatisticsHelper.GetVaccinationsPerWeekDesoNames(desoDto);


           
           
            
            int year2020 = 2020;
            int year2021 = 2021;
            int year2022 = 2022;
            int year2023 = 2023;
            int year2024 = 2024;
           
            vaccinationsPerWeekDeso2020 = StatisticsHelper.GetVaccinationsPerWeekDeso(desoCodeDto.VaccinationDesoDtos, year2020);
            vaccinationsPerWeekDeso2021 = StatisticsHelper.GetVaccinationsPerWeekDeso(desoCodeDto.VaccinationDesoDtos, year2021);
            vaccinationsPerWeekDeso2022 = StatisticsHelper.GetVaccinationsPerWeekDeso(desoCodeDto.VaccinationDesoDtos, year2022);
            vaccinationsPerWeekDeso2023 = StatisticsHelper.GetVaccinationsPerWeekDeso(desoCodeDto.VaccinationDesoDtos, year2023);
            vaccinationsPerWeekDeso2024 = StatisticsHelper.GetVaccinationsPerWeekDeso(desoCodeDto.VaccinationDesoDtos, year2024);

            vaccinationsPerWeek2020 = StatisticsHelper.GetVaccinationsPerWeek(desoCodeDto.VaccinationDesoDtos,  year2020);
            vaccinationsPerWeek2021 = StatisticsHelper.GetVaccinationsPerWeek(desoCodeDto.VaccinationDesoDtos, year2021);
            vaccinationsPerWeek2022 = StatisticsHelper.GetVaccinationsPerWeek(desoCodeDto.VaccinationDesoDtos, year2022);
            vaccinationsPerWeek2023 = StatisticsHelper.GetVaccinationsPerWeek(desoCodeDto.VaccinationDesoDtos, year2023);
            vaccinationsPerWeek2024 = StatisticsHelper.GetVaccinationsPerWeek(desoCodeDto.VaccinationDesoDtos, year2024);
        }
        
    }
}
