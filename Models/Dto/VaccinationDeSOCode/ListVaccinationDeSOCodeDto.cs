using Newtonsoft.Json;
namespace DSU24_Grupp5.Models.Dto.VaccinationDeSOCode
{
    public class ListVaccinationDeSOCodeDto
    {
        [JsonProperty(PropertyName = "date-of-vaccination")]
        public string DateOfVaccination {  get; set; }

        [JsonProperty(PropertyName = "batch-number")]
        public string BatchNumber { get; set; }

        [JsonProperty(PropertyName = "dose-number")]
        public int DoseNumber { get; set; }

        [JsonProperty(PropertyName = "vaccination-site")]
        public VaccinationSiteDeSODto VaccinationSiteDeSO { get; set; }
    }
}
