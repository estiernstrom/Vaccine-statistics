using DSU24_Grupp5.Controllers;
using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.Batches;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.Sites;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.HeadModel;
using DSU24_Grupp5.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Azure.Amqp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DSU24_Grupp5.ApiResponseProcess
{

    public static class StatisticsHelper
    {
      
        private readonly static string JsonDesoFilePath = "JsonFiles\\Deso.json";
        private readonly static string BatchesFilePath = "JsonFiles\\Batches.json";
        private readonly static string SitesFilePath = "JsonFiles\\Site.json";

        public static int CountMostAmountOfVaccinations() 
        {
           Singleton singleton1 = Singleton.GetSingleton();
           int mostAmountOfVaccinations = 0;
            
            foreach (var item in singleton1.Patients)
            {
                if (item.TotalVaccinations > mostAmountOfVaccinations)
                {
                    mostAmountOfVaccinations = item.TotalVaccinations;
                }
            }
            return mostAmountOfVaccinations;
        }

        /// <summary>
        /// Plockar ut deso-namnen från objekten i listan som skickas in 
        /// returnerar en lista med enbart deso-namnen
        /// </summary>
        public static List<string> DesoNames(List<AreaDesoDto> areaDesoDtos, IndexFilterViewModel indexFilterViewModel) 
        {
            List<string> desoNames = new();

            desoNames = indexFilterViewModel.DesoCodes.SelectMany(c => areaDesoDtos
                                                      .Where(a => a.Deso
                                                      .Contains(c))
                                                      .Select(a => a.DesoName))
                                                      .Distinct()
                                                      .ToList();
            return desoNames;
        }

        public static List<string> DesoCodes(List<AreaDesoDto> areasDesoDtos, IndexFilterViewModel indexFilterViewModel)
        {
            List<string> desoCodes = new();

            desoCodes = indexFilterViewModel.DesoCodes.SelectMany(c => areasDesoDtos
                                                      .Where(a => a.Deso
                                                      .Contains(c))
                                                      .Select(a => a.Deso))
                                                      .Distinct()
                                                      .ToList();

            return desoCodes;
        }

       /// <summary>
       /// Method that counts foreign born population and swedish born from SCB for each Deso Area.
       /// </summary>
       /// 
        public static void HeritagePopulationByDeso()
        {
            List<string> population = new List<string>();
            ScbDto scbDto = new ScbDto();
            string desoCodes = File.ReadAllText(JsonDesoFilePath);
            DesoDto desoDto = JsonSerializer.Deserialize<DesoDto>(desoCodes);
            scbDto = JsonFileClient.GetScbBackgroundByDesoFromJson();
            List<int> swedishBackground = new List<int>();
            List<int> foreignBackground = new List<int>();

            foreach (var deso in desoDto.Areas)
            {
                swedishBackground.Add( scbDto.Data.Where(k => k.Key[1] == "2" && k.Key[0] == deso.Deso)
                                                    .Sum(x => int.Parse(x.Values[0])));

                foreignBackground.Add( scbDto.Data.Where(k => k.Key[1] == "1" && k.Key[0] == deso.Deso)
                                                    .Sum(x => int.Parse(x.Values[0])));
            }

            List<double> popDesoPercentageForeign = PopulationForeignByDesoPercentage(swedishBackground, foreignBackground);
            List<double> popDesoPercentageSwe = PopulationSweByDesoPercentage(swedishBackground, foreignBackground);
        }

        /// <summary>
        /// Method that summarize swedish born the poplation in percentage.
        /// </summary>
        /// 
        public static List<double> PopulationSweByDesoPercentage(List<int> swePop, List<int> foreignPop)
        {
            List<double> popDesoPercentage = new List<double>();

            for (int i = 0; i < swePop.Count; i++) 
            {
                double totalPop = swePop[i] + foreignPop[i];
                double popSwe = swePop[i] / totalPop;
                double total = (popSwe * 100);
                popDesoPercentage.Add(Math.Round(total, 1));
            }
            return popDesoPercentage;
        }

        /// <summary>
        /// Method that summarize foreign born the poplation in percentage.
        /// </summary>
        public static List<double> PopulationForeignByDesoPercentage(List<int> swePop, List<int> foreignPop)
        {
            List<double> popDesoPercentage = new List<double>();

            for (int i = 0; i < swePop.Count; i++)
            {
                double totalPop = swePop[i] + foreignPop[i];
                double popForeign = foreignPop[i] / totalPop;
                double total = (popForeign * 100);
                popDesoPercentage.Add(Math.Round(total, 1));
            }
            return popDesoPercentage;
        }

        public static List<int> AgeCounter(IndexFilterViewModel indexFilterViewModel, List<string> desoCodes)
        {
            List<int> sortedAges = new();
            int[] ageRanges = { 15, 25, 35, 45, 55, 65, 75 };
            Singleton singleton1 = Singleton.GetSingleton();
            int loopStopper = 1;

            for (int i = 0; i < ageRanges.Count(); i++)
            {
                if (i == ageRanges.Count() - 1)
                {
                    var lastCount = singleton1.Patients.Where(p => (desoCodes.Contains(p.DesoCode)) && 
                                                        (indexFilterViewModel.Gender ?? p.Gender) == p.Gender)
                                                        .SelectMany(v => v.Vaccinations)
                                                        .Where(v => (v.AgeWhenVaccinate >= ageRanges[i]) && 
                                                        (v.DoseNumber == indexFilterViewModel.DoseNumber) && 
                                                        (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                        (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                        (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                        (indexFilterViewModel.VaccinationStation ?? v.SiteName) == v.SiteName)
                                                        .Count();
                    sortedAges.Add(lastCount);
                }
                else
                {
                    var count = singleton1.Patients.Where(p => (desoCodes.Contains(p.DesoCode)) &&
                                                   (indexFilterViewModel.Gender ?? p.Gender) == p.Gender)
                                                   .SelectMany(v => v.Vaccinations)
                                                   .Where(v => (v.AgeWhenVaccinate >= ageRanges[i]) && 
                                                   (v.AgeWhenVaccinate < ageRanges[i+loopStopper]) &&
                                                   (v.DoseNumber == indexFilterViewModel.DoseNumber) &&
                                                   (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                   (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                   (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                   (indexFilterViewModel.VaccinationStation ?? v.SiteName) == v.SiteName)
                                                   .Count();
                    sortedAges.Add(count);
                }
            }
            return sortedAges;
        }

        /// <summary>
        /// Method that counts the total amount of vaccinated by given paramethers.
        /// </summary>
        /// <returns>
        /// 
        public static int TotalAmountOfVaccinated(List<string> desoCodes, IndexFilterViewModel indexFilterViewModel)
        {
            int vaccinatedByDose = 0;
            Singleton singleton1 = Singleton.GetSingleton();

            foreach (var deso in desoCodes)
            {
                vaccinatedByDose = singleton1.Patients.Where(p => (desoCodes.Contains(p.DesoCode)) &&
                                                (indexFilterViewModel.Gender ?? p.Gender) == p.Gender)
                                                .SelectMany(v => v.Vaccinations)
                                                .Where(v => (v.DoseNumber >= indexFilterViewModel.DoseNumber) &&
                                                (v.DoseNumber == indexFilterViewModel.DoseNumber) &&
                                                (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                (indexFilterViewModel.VaccinationStation ?? v.SiteName) == v.SiteName)
                                                .Count();
            }
            return vaccinatedByDose;
        }

        /// <summary>
        /// Method that counts total population from SCB with given paramethers
        /// </summary>
        public static int TotalPopulationCountScb(List<string> desoCode, ScbDto scbDto)
        {
            int populationCount = 0;

            foreach (string deso in desoCode)
            {
                populationCount += scbDto.Data.Where(k => k.Key[0] == deso)
                                                  .Sum(x => int.Parse(x.Values[0]));
            }

            return populationCount;
        }

        /// <summary>
        /// Method that counts population percentage with given paramethers
        /// </summary>
        public static double TotalAmountOfVaccinatedPercentage(int totalCountScb, int amountOfVaccinated)
        {
            double percentage = (double)amountOfVaccinated / totalCountScb;
            double total = (percentage * 100);

            return Math.Round(total, 1);

        }

        public static bool CheckAgeInterval(int[] ageInterval, int ageWhenVaccinate)
        {
                foreach (int age in ageInterval)
                {
                    if (age == ageWhenVaccinate)
                    {
                        return true;
                    }
                    else if (age == 75 && ageWhenVaccinate >= 75)
                    {
                    return true; 
                    }  
                }  
            return false;
        }

        public static int [] ConvertStringToIntArray(string ageInterval)
        {
            int[] selectedAgeInterval = Array.ConvertAll(ageInterval.Split(','), int.Parse);

            return selectedAgeInterval;
        }

        public static List<int> GenderCounter(IndexFilterViewModel indexFilterViewModel, List<string> desoNames)
        {
            
            Singleton singleton1 = Singleton.GetSingleton();

            int[] selectedAgeInterval = [0];
            selectedAgeInterval = ConvertStringToIntArray(indexFilterViewModel.AgeInterval);

            int maleVaccinatedCount = 0;
            int femaleVaccinatedCount = 0;
            int nonBinaryVaccinatedCount = 0;
            List<int> genderVaccinatedCounter = new List<int>();

            foreach (var deso in desoNames)
            {
                maleVaccinatedCount += singleton1.Patients.Where(p => p.Gender == "Male" && p.DesoName == deso)
                                                         .SelectMany(v => v.Vaccinations)
                                                         .Where(v => (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                          (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                          (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                          CheckAgeInterval(selectedAgeInterval, v.AgeWhenVaccinate) &&
                                                          (indexFilterViewModel.DoseNumber == v.DoseNumber)).Count();

                femaleVaccinatedCount += singleton1.Patients.Where(p => p.Gender == "Female" && p.DesoName == deso)
                                                           .SelectMany(v => v.Vaccinations)
                                                           .Where(v => (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                           (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                           (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                           CheckAgeInterval(selectedAgeInterval, v.AgeWhenVaccinate) &&
                                                           (indexFilterViewModel.DoseNumber == v.DoseNumber)).Count();

                nonBinaryVaccinatedCount += singleton1.Patients.Where(p => p.Gender == "Other" && p.DesoName == deso)
                                                              .SelectMany(v => v.Vaccinations)
                                                              .Where(v => (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                                               (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                                               (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                                               CheckAgeInterval(selectedAgeInterval, v.AgeWhenVaccinate) &&
                                                               (indexFilterViewModel.DoseNumber == v.DoseNumber)).Count();
            }
            
            genderVaccinatedCounter.Add(maleVaccinatedCount);
            genderVaccinatedCounter.Add(femaleVaccinatedCount);
            genderVaccinatedCounter.Add(nonBinaryVaccinatedCount);
            return genderVaccinatedCounter;
        }

        public static bool AgeIntervalContain(string selectedAgeInterval, string scbAgeInterval)
        {
          
            string[] values = selectedAgeInterval.Split(',');
            foreach (string value in values)
            {
                if (scbAgeInterval.Contains(value))
                {
                    return true;
                }
            }
            return false; 
        }

        public static List<int> GenderTotalCountScb(IndexFilterViewModel indexFilterViewModel, List<string> desoCode, ScbDto scbDto)
        {
            List<int> totalPopulationByGender = new List<int>();
            
            int malePopulationCount = 0;
            int femalePopulationCount = 0;

            foreach (string deso in desoCode)
            {

                malePopulationCount += scbDto.Data.Where(k => k.Key[2] == "1" && k.Key[0] == deso && AgeIntervalContain(indexFilterViewModel.AgeInterval, k.Key[1]))
                                       .Sum(x => int.Parse(x.Values[0]));

                femalePopulationCount += scbDto.Data.Where(k => k.Key[2] == "2" && k.Key[0] == deso && AgeIntervalContain(indexFilterViewModel.AgeInterval, k.Key[1]))
                                                    .Sum(x => int.Parse(x.Values[0]));

            }

            totalPopulationByGender.Add(malePopulationCount);
            totalPopulationByGender.Add(femalePopulationCount);

            return totalPopulationByGender;
        }

        public static List<int> GenderNotVaccinated(List<int> populationVaccinatedByGender, List <int> genderScbPopulation)
        {
            List<int> genderNotVaccinatedTotal = new List<int>();

            genderNotVaccinatedTotal.Add(genderScbPopulation[0] - populationVaccinatedByGender[0]);
            genderNotVaccinatedTotal.Add(genderScbPopulation[1] - populationVaccinatedByGender[1]);
            return genderNotVaccinatedTotal;
        }

        /// <summary>
        /// Method that counts total amount of vaccinated by each Deso Area. 
        /// </summary
        public static List<int> GetRecordsPerDeso(List<string> desoNames, IndexFilterViewModel indexFilterViewModel)
        {
            List<int> totalRecordsPerDeso = new();
            int totalRecordDeso;
            Singleton singleton1 = Singleton.GetSingleton();

            int[] selectedAgeInterval = [0];
            selectedAgeInterval = ConvertStringToIntArray(indexFilterViewModel.AgeInterval);

            foreach (var name in desoNames)
            {
                totalRecordDeso = singleton1.Patients.Where(p => (p.DesoName == name) && 
                                               (indexFilterViewModel.Gender ?? p.Gender) == p.Gender) 
                                               .SelectMany(v => v.Vaccinations)
                                               .Where(v => (v.DoseNumber == indexFilterViewModel.DoseNumber) &&
                                               (indexFilterViewModel.VaccineName ?? v.VaccinName) == v.VaccinName &&
                                               (indexFilterViewModel.BatchNumber ?? v.BatchNumber) == v.BatchNumber &&
                                               (indexFilterViewModel.Manufacturer ?? v.Manufacturer) == v.Manufacturer &&
                                               CheckAgeInterval(selectedAgeInterval, v.AgeWhenVaccinate) &&
                                               (indexFilterViewModel.VaccinationStation ?? v.SiteName) == v.SiteName)
                                               .Count();


                totalRecordsPerDeso.Add(totalRecordDeso);
            }
            return totalRecordsPerDeso;
        }
     
        public static List<int> PopulationByDesoCodes(List<string> desoCodes, ScbDto scbDto, IndexFilterViewModel indexFilterViewModel)
        {
            List<int> populationDesoAreas = new List<int>();
            int populationCountDeso = 0;
            string? gender = null;
            if (indexFilterViewModel.Gender != null)
            {
                if (indexFilterViewModel.Gender == "Male")
                {
                    gender = "1";
                }
                else if (indexFilterViewModel.Gender == "Female")
                {
                    gender = "2";
                }
            }
            foreach (var code in desoCodes)
            {
                populationCountDeso = scbDto.Data.Where(k => (k.Key[0] == code) && 
                                                 (AgeIntervalContain(indexFilterViewModel.AgeInterval, k.Key[1])) && 
                                                 (gender ?? k.Key[2]) == k.Key[2])
                                                 .Sum(x => int.Parse(x.Values[0]));
                populationDesoAreas.Add(populationCountDeso);
            }

            return populationDesoAreas;
        }

        public static string? DesoNameByDesoCode(string desoCode)
        {
            Singleton singleton1 = Singleton.GetSingleton();
            string desoName = singleton1.Patients.Where(p => p.DesoCode == desoCode).Select(d => d.DesoName).FirstOrDefault();
            
            return desoName;
        }

        public static string OneDesoNameByDesoCode(string desoCode, DesoDto desoDto)
        {
           string desoName = desoDto.Areas.Where(d => d.Deso == desoCode).Select(name => name.DesoName).First();
           return desoName;
        }

        public static BatchesDto GetBatchNumbers()
        {
            List<string> batchNumber = new();
            string batches = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batches);

            return batchesDto;
        }

        public static List<string> GetBatchNumberByVaccineName(string selectedVaccineName)
        {
            List<string> batchNames = new();
   
            BatchesDto batchesDto = GetBatchNumbers();

            foreach (var batches in batchesDto.Batches)
            {
                if (batches.VaccineName == selectedVaccineName)
                {
                    batchNames.Add(batches.BatchNumber);
                }
            }
            return batchNames;
        }

        public static List<string> GetManufacturerByVaccineName(string selectedVaccine)
        {
            List<string> manufacturers = new();
            string batch = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batch);

            manufacturers = batchesDto.Batches.Where(b => b.VaccineName == selectedVaccine).Select(b => b.Manufacturer).Distinct().ToList();

            return manufacturers;
        }

        public static List<string> GetVaccineNameByManufacturer(string selectedManufacturer)
        {
            List<string> vaccineNames = new();
            string batch = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batch);

            vaccineNames = batchesDto.Batches.Where(b => b.Manufacturer == selectedManufacturer).Select(b => b.VaccineName).Distinct().ToList();

            return vaccineNames;
        }

        public static List<string> GetVaccineNames()
        {
            List<string> vaccineName = new();
            string batches = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batches);

            vaccineName = batchesDto.Batches.Select(b => b.VaccineName).Distinct().ToList();

            return vaccineName;
        }

        public static List<string> GetManufacturers()
        {
            List<string> manufacturerName = new();
            string batches = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batches);

            manufacturerName = batchesDto.Batches.Select(b => b.Manufacturer).Distinct().ToList();

            return manufacturerName;
        }

        public static List<List<DateTime>> CalculateWeeks(int year)
        {
            

            List<List<DateTime>> weeksWithStartDates = new List<List<DateTime>>();
          
          
            DateTime firstDayOfYear = new DateTime(year, 1, 1);
            

            int currentWeek = 0;
            DateTime currentDate = firstDayOfYear;
            
            
                while (currentWeek < 52)
                {
                    DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
                    var weekDates = new List<DateTime>();

                    for (int i = 0; i < 7; i++)
                    {
                        weekDates.Add(startOfWeek.AddDays(i));
                    }

                    weeksWithStartDates.Add(weekDates);
                    currentDate = startOfWeek.AddDays(7);
                    currentWeek++;
                }
            
            
            
            return weeksWithStartDates;
        }

        public static List<int> GetVaccinationsPerWeek(List<VaccinationDesoDto> vaccinationDesoDtos, int year)
        {
        
           
            List<List<DateTime>> weeks = CalculateWeeks(year);
            int[] weeksArray = new int[52];
            foreach(var deso in vaccinationDesoDtos)
            {
                foreach(var patient in deso.Patients)
                {
                    foreach(var vaccination in patient.Vaccinations)
                    {
                        DateTime dateOfVaccination = DateTime.Parse(vaccination.DateOfVaccination);

                        for(int i = 0; i<weeks.Count; i++)
                        {

                            if (weeks[i].Any(date => date <= dateOfVaccination && dateOfVaccination < date.AddDays(7)))
                            {
                                weeksArray[i]++;
                                break;
                            }
                        }

                    }
                }
            }
        
            return weeksArray.ToList(); 
        }

        public static List<List<int>> GetVaccinationsPerWeekDeso(List<VaccinationDesoDto> vaccinationDesoDtos, int year )
        {

            

            List<List<int>> DesoCurveList = new List<List<int>>();



            List<List<DateTime>> weeks = CalculateWeeks(year);



          

            foreach (var deso in vaccinationDesoDtos)
            {
                int[] weeksArray = new int[52];
                foreach (var patient in deso.Patients)
                {
                    foreach (var vaccination in patient.Vaccinations)
                    {
                        DateTime dateOfVaccination = DateTime.Parse(vaccination.DateOfVaccination);

                        for (int i = 0; i < weeks.Count; i++)
                        {

                            if (weeks[i].Any(date => date <= dateOfVaccination && dateOfVaccination < date.AddDays(7)))
                            {
                                weeksArray[i]++;

                                break;
                            }
                        }

                    }
                }

                DesoCurveList.Add(weeksArray.ToList());

            }



            return DesoCurveList;
        }

        public static List<string> GetVaccinationsPerWeekDesoNames(DesoDto desoDto)
        {
            List<string> DesoCurveName = new List<string>();
            foreach (var desoName in desoDto.Areas)
            {
                DesoCurveName.Add(desoName.DesoName);
            }
            return DesoCurveName;
        }

        public static string GetVaccineName(string batchNumber, BatchesDto batchesDto)
        {
            string batchName = batchesDto.Batches.Where(b => b.BatchNumber == batchNumber).Select(name => name.VaccineName).First();
            return batchName;
        }

        public static DesoDto GetListOfDesoAreas(DesoDto desoDto)
        {
            List<string> desoNames = new List<string>();

            desoNames = desoDto.Areas.Select(b => b.DesoName).Distinct().ToList();

            return desoDto;
        }
        
        public static List<string> GetListOfVaccStations()
        {
            List<string> listOfVaccStations = new List<string>();

            string vaccStation = File.ReadAllText(SitesFilePath);
            SitesDto[] sitesDto = JsonSerializer.Deserialize<SitesDto[]>(vaccStation);
           
            foreach(var site in sitesDto)
            {
                listOfVaccStations.Add(site.Name);
            }
            return listOfVaccStations;    
        }

        internal static string? GetManufacturerName(string batchNumber, BatchesDto batchesDto)
        {
            string manufacturerName = batchesDto.Batches.Where(b => b.BatchNumber == batchNumber).Select(name => name.Manufacturer).First();
            return manufacturerName;
        }

        internal static int AgeWhenVaccinated(int yearOfBirth, string dateOfVaccination)
        {
            int ageWhenVaccinated = DateTime.Parse(dateOfVaccination).Year - yearOfBirth;
            return ageWhenVaccinated;
        }
    }
}