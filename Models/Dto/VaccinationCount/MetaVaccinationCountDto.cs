using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DSU24_Grupp5.Models.Dto.VaccinationCount
{
    public class MetaVaccinationCountDto
    {
        [JsonProperty(PropertyName = "total-deso-areas")]
        public int TotalDesoAreas { get; set; }
        [JsonProperty(PropertyName = "total-records")]
        public int TotalRecords { get; set; }
    }
}
