using Newtonsoft.Json;

namespace DSU24_Grupp5.Models.Dto.Batches
{
    public class MetaBatchesDto
    {
        [JsonProperty(PropertyName = "total-records")]
        public int TotalRecords { get; set; }
    }
}
