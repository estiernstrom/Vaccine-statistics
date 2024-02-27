using DSU24_Grupp5.ApiResponseProcess;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.Batches;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.Sites;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using DSU24_Grupp5.Models.HeadModel;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using static DSU24_Grupp5.Infrastructure.JsonFileClient;

namespace DSU24_Grupp5.Infrastructure
{
    public static class ApiClient
    {


        #region ApiCalls
        public static async Task GetGenderAgeTotalFromSCBApi()
        {
            string apiUrl = "https://api.scb.se/OV0104/v1/doris/sv/ssd/START/BE/BE0101/BE0101A/BefolkningNy";
            string body = File.ReadAllText("JsonFiles/Post/scbAgeGenderTotal.json");
            var scbResponse = await ApiEngine.Post<ScbDto>(apiUrl, body);

            CreateScbPopulationByAgeAndGenderJson(scbResponse);
        }
        public static async Task GetAgeGenderByDesoFromSCBApi()
        {
            string apiUrl = "https://api.scb.se/OV0104/v1/doris/sv/ssd/START/BE/BE0101/BE0101Y/FolkmDesoAldKonN";
            string body = File.ReadAllText("JsonFiles/Post/scbAgeGenderByDeso.json");
            var scbResponse = await ApiEngine.Post<ScbDto>(apiUrl, body);

            CreateScbByAgeAndGenderJson(scbResponse);
        }

        public static async Task GetBackgroundByDesoFromSCBApi()
        {
            string apiUrl = "https://api.scb.se/OV0104/v1/doris/sv/ssd/START/BE/BE0101/BE0101Y/FolkmDesoBakgrKonN";
            string body = File.ReadAllText("JsonFiles/Post/scbBackgroundPost.json");
            var scbResponse = await ApiEngine.Post<ScbDto>(apiUrl, body);

            CreateBackgroundByDesoFromSCBJson(scbResponse);
        }

        public static async Task GetDesoInfo()
        {
            string apiUrl = "https://grupp1.dsvkurs.miun.se/api/deso";
            var desoResponse = await ApiEngine.Fetch<DesoDto>(apiUrl);

            CreateDesoJson(desoResponse);
        }

        public static async Task GetSiteInfo()
        {
            string apiUrl = "https://grupp1.dsvkurs.miun.se/api/sites";
            var siteResponse = await ApiEngine.Fetch<SitesDto[]>(apiUrl);

            CreateSiteJson(siteResponse);
        }

        public static async Task GetVaccinationFromDesoCode()
        {
            DesoDto desoDto = GetDesoInfoFromJson();
            List<VaccinationDesoDto> vaccinationDesoDtos = new();

            foreach (var deso in desoDto.Areas)
            {
                string apiUrl = $"https://grupp1.dsvkurs.miun.se/api/vaccinations/{deso.Deso}";
                var desoResponse = await ApiEngine.Fetch<VaccinationDesoDto>(apiUrl);

                vaccinationDesoDtos.Add(desoResponse.Data);
            }

            DesoCodeDto desoCodeDto = new DesoCodeDto(vaccinationDesoDtos);
            CreateVaccinationDesoCodeJson(desoCodeDto);
        }
        public static async Task VaccinationCount()
        {
            string apiUrl = "https://grupp1.dsvkurs.miun.se/api/vaccinations/count"; 
            var vaccinationsResponse = await ApiEngine.Fetch<VaccinationCountDto>(apiUrl);

            VaccinationCountDto vaccinationCountDesoDto = vaccinationsResponse.Data;
            CreateVaccinationCountJson(vaccinationCountDesoDto);
        }
        public static async Task GetBatchesInfo()
        {
            string apiUrl = "https://grupp1.dsvkurs.miun.se/api/batches";
            var batchesResponse = await ApiEngine.Fetch<BatchesDto>(apiUrl);

            CreateBatchesJson(batchesResponse);
        }
        #endregion


        public static List<Patient> CreateListOfAllPatients()
        {
            List<Patient> patientList = new List<Patient>();

            DesoCodeDto desoCodeDto = GetAllVaccinationDesoDataFromJson();
            DesoDto desoDto = GetDesoInfoFromJson();
            BatchesDto batchesDto = GetBatchesFromJson();

            foreach (var item in desoCodeDto.VaccinationDesoDtos)
            {
                foreach (var p in item.Patients)
                {
                    Patient patient = new Patient();
                    patient.Gender = p.Gender;
                    patient.DesoCode = item.Meta.DesoCode;
                    patient.DesoName = StatisticsHelper.OneDesoNameByDesoCode(patient.DesoCode, desoDto);
                    patient.Vaccinations = new List<Vaccination>();
                    foreach (var v in p.Vaccinations)
                    {
                        Vaccination vaccination = new Vaccination();
                        vaccination.DateOfVaccination = v.DateOfVaccination;
                        vaccination.AgeWhenVaccinate = StatisticsHelper.AgeWhenVaccinated(p.YearOfBirth, v.DateOfVaccination);
                        vaccination.DoseNumber = v.DoseNumber;
                        vaccination.BatchNumber = v.BatchNumber;
                        vaccination.VaccinName = StatisticsHelper.GetVaccineName(vaccination.BatchNumber, batchesDto);
                        vaccination.Manufacturer = StatisticsHelper.GetManufacturerName(vaccination.BatchNumber, batchesDto);
                        vaccination.SiteName = v.VaccinationSiteDeSO.SiteName;
                        patient.Vaccinations.Add(vaccination);
                    }
                    patient.TotalVaccinations = patient.Vaccinations.Count();
                    patientList.Add(patient);
                }
            }
            return patientList;
        }
    }
}
