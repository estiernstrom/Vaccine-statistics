using DSU24_Grupp5.Models;
using DSU24_Grupp5.Models.Dto.Batches;
using DSU24_Grupp5.Models.Dto;
using DSU24_Grupp5.Models.Dto.DeSO;
using DSU24_Grupp5.Models.Dto.SCB;
using DSU24_Grupp5.Models.Dto.Sites;
using DSU24_Grupp5.Models.Dto.VaccinationCount;
using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.Options;

namespace DSU24_Grupp5.Infrastructure
{
    public static class JsonFileClient
    {
        private readonly static string JsonVaccinationCount = "JsonFiles\\VaccinationCount.json";
        private readonly static string JsonDesoFilePath = "JsonFiles\\Deso.json";
        private readonly static string JsonVaccinationDesoCodeFilePath = "JsonFiles\\VaccinationDesoCode.json";
        private readonly static string ScbPopulationByAgeAndGenderFilePath = "JsonFiles\\ScbPopulationByAgeAndGender.json";
        private readonly static string ScbAgeAndGenderByDesoFilePath = "JsonFiles\\ScbAgeAndGenderByDeso.json";
        private readonly static string ScbBackgroundByDesoFilePath = "JsonFiles\\scbBackgroundByDeso.json";
        private readonly static string BatchesFilePath = "JsonFiles\\Batches.json";
        private readonly static string SitesFilePath = "JsonFiles\\Site.json";
        
        #region JsonSerializer options
        private static JsonSerializerOptions JsonSerializerOptions()
        {
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowCharacters('\u00D6', '\u00F6', '\u00E4', '\u00C4', '\u00E5', '\u00C5', '\u002B', '\u02B2', '\u002B');
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
            var options2 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                WriteIndented = true
            };

            return options2;
        }
        #endregion

        #region JsonSerialize
        public static void CreateScbPopulationByAgeAndGenderJson(ApiResponse<ScbDto> scbResponse)
        {
            File.WriteAllText(ScbPopulationByAgeAndGenderFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(scbResponse.Data, options);
            File.WriteAllText(ScbPopulationByAgeAndGenderFilePath, text);
        }

        public static void CreateScbByAgeAndGenderJson(ApiResponse<ScbDto> scbResponse)
        {
            File.WriteAllText(ScbAgeAndGenderByDesoFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(scbResponse.Data, options);
            File.WriteAllText(ScbAgeAndGenderByDesoFilePath, text);
        }

        public static void CreateBackgroundByDesoFromSCBJson(ApiResponse<ScbDto> scbResponse)
        {
            File.WriteAllText(ScbBackgroundByDesoFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(scbResponse.Data, options);
            File.WriteAllText(ScbBackgroundByDesoFilePath, text);
        }

        public static void CreateDesoJson(ApiResponse<DesoDto> desoResponse)
        {
            File.WriteAllText(JsonDesoFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(desoResponse.Data, options);
            File.WriteAllText(JsonDesoFilePath, text);
        }

        public static void CreateSiteJson(ApiResponse<SitesDto[]> siteResponse)
        {
            File.WriteAllText(SitesFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(siteResponse.Data, options);
            File.WriteAllText(SitesFilePath, text);
        }

        public static void CreateVaccinationDesoCodeJson(DesoCodeDto desoCodeResponse)
        {
            File.WriteAllText(JsonVaccinationDesoCodeFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(desoCodeResponse.VaccinationDesoDtos, options);
            File.WriteAllText(JsonVaccinationDesoCodeFilePath, text);
        }

        public static void CreateVaccinationCountJson(VaccinationCountDto vaccinationCountDesoDto)
        {
            File.WriteAllText(JsonVaccinationCount, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(vaccinationCountDesoDto, options);
            File.WriteAllText(JsonVaccinationCount, text);
        }

        public static void CreateBatchesJson(ApiResponse<BatchesDto> batchesResponse)
        {
            File.WriteAllText(BatchesFilePath, string.Empty);
            var options = JsonSerializerOptions();
            string text = JsonSerializer.Serialize(batchesResponse.Data, options);
            File.WriteAllText(BatchesFilePath, text);
        }



        #endregion

        #region JsonDeserializes
        public static DesoCodeDto GetAllVaccinationDesoDataFromJson()
        {
            string JsonVaccinationDesoCode = File.ReadAllText(JsonVaccinationDesoCodeFilePath);
            List<VaccinationDesoDto> vaccinationDesoDtos = JsonSerializer.Deserialize<List<VaccinationDesoDto>>(JsonVaccinationDesoCode);
            DesoCodeDto desoCodeDto = new DesoCodeDto(vaccinationDesoDtos);
            return desoCodeDto;
        }

        public static DesoDto GetDesoInfoFromJson()
        {
            string JsonDeso = File.ReadAllText(JsonDesoFilePath);
            DesoDto desoDto = JsonSerializer.Deserialize<DesoDto>(JsonDeso);

            return desoDto;
        }

        public static BatchesDto GetBatchesFromJson()
        {
            string batches = File.ReadAllText(BatchesFilePath);
            BatchesDto batchesDto = JsonSerializer.Deserialize<BatchesDto>(batches);
            return batchesDto;
        }

        public static ScbDto GetScbAgeAndGenderByDesoFromJson()
        {
            string scbPopulationByDesoAreas = File.ReadAllText(ScbAgeAndGenderByDesoFilePath);
            ScbDto scbDtos = JsonSerializer.Deserialize<ScbDto>(scbPopulationByDesoAreas);
            return scbDtos;
        }

        public static ScbDto GetScbPopulationByAgeAndGenderFromJson()
        {
            string scbPopulationByAgeAndGender = File.ReadAllText(ScbPopulationByAgeAndGenderFilePath);
            ScbDto scbDtos = JsonSerializer.Deserialize<ScbDto>(scbPopulationByAgeAndGender);
            return scbDtos;
        }

        public static VaccinationCountDto GetVaccinationCountFromJson()
        {
            string JsonVaccinationCountText = File.ReadAllText(JsonVaccinationCount);
            VaccinationCountDto vaccinationCountDesoDto = JsonSerializer.Deserialize<VaccinationCountDto>(JsonVaccinationCountText);
            return vaccinationCountDesoDto;
        }

        public static ScbDto GetScbBackgroundByDesoFromJson()
        {
            string scbBackgroundByDeso = File.ReadAllText(ScbBackgroundByDesoFilePath);
            ScbDto scbDtos = JsonSerializer.Deserialize<ScbDto>(scbBackgroundByDeso);
            return scbDtos;
        }

        #endregion

    }
}
