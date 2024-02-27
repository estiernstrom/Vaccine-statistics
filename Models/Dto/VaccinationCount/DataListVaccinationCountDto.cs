using Newtonsoft.Json;

namespace DSU24_Grupp5.Models.Dto.VaccinationCount
{
    public class DataListVaccinationCountDto
    {
        [JsonProperty(PropertyName = "dose-number")]
        public int DoseNumber { get; set; }
        public int Count { get; set; }
    }
}
