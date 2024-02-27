using Newtonsoft.Json;
namespace DSU24_Grupp5.Models.Dto.Batches
{
    public class ListOfBatchesDto
    {
        [JsonProperty(PropertyName = "batch-number")]
        public string BatchNumber { get; set; }

        [JsonProperty(PropertyName = "vaccine-name")]
        public string VaccineName { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "total-uses")]
        public int TotalUses { get; set; }
    }
}
