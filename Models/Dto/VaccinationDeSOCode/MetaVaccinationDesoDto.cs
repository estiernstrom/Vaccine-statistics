using Newtonsoft.Json;

namespace DSU24_Grupp5.Models.Dto
{
    public class MetaVaccinationDesoDto
    {
        [JsonProperty(PropertyName= "total-records-patients")]
        public int TotalRecordsPatients { get; set; }

        [JsonProperty(PropertyName = "total-vaccinations")]
        public int TotalVaccinations { get; set; }

        [JsonProperty(PropertyName = "deso-code")]
        public string DesoCode { get; set; }

        [JsonProperty(PropertyName = "latest-change")]
        public string LatestChange { get; set; }
    }
}