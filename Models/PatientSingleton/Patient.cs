using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;

namespace DSU24_Grupp5.Models.HeadModel
{
    public class Patient
    {
        public string? DesoCode { get; set; }
        public string? DesoName { get; set; }
        public string? Gender { get; set; }
        public int TotalVaccinations { get; set; }
        public List<Vaccination>? Vaccinations { get; set; }
    }
}
