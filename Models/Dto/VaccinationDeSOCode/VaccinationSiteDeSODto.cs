using Newtonsoft.Json;
namespace DSU24_Grupp5.Models.Dto.VaccinationDeSOCode
{
    public class VaccinationSiteDeSODto
    {
        [JsonProperty(PropertyName = "site-id")]
        public int SiteId { get; set; }

        [JsonProperty(PropertyName = "site-name")]
        public string SiteName { get; set; }
    }
}
