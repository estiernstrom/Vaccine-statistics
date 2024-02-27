using Newtonsoft.Json;
namespace DSU24_Grupp5.Models.Dto.VaccinationDeSOCode
{
    public class PatientVaccinationDto
    {
        [JsonProperty(PropertyName = "year-of-birth")]
        public int YearOfBirth { get; set; }

        public string Gender { get; set; }

        public List<ListVaccinationDeSOCodeDto> Vaccinations { get; set; }


    }    
}
