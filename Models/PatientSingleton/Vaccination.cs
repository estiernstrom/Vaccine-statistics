using System.Security.Principal;

namespace DSU24_Grupp5.Models.HeadModel
{
    public class Vaccination
    {
        public int AgeWhenVaccinate { get; set; }
        public string? DateOfVaccination { get; set; }
        public int DoseNumber { get; set; }
        public string? BatchNumber { get; set; }
        public string? VaccinName { get; set; }
        public string? Manufacturer { get; set; }
        public string? SiteName { get; set; }
    }
}