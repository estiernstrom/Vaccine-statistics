using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DSU24_Grupp5.Models.Dto.DeSO
{
    public class AreaDesoDto
    {
        public string Deso { get; set; }
        [JsonProperty(PropertyName = "deso-name")]
        public string DesoName { get; set; }
    }
}