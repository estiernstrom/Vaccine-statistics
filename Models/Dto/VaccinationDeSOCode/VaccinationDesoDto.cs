using DSU24_Grupp5.Models.Dto.VaccinationDeSOCode;

namespace DSU24_Grupp5.Models.Dto
{
    public class VaccinationDesoDto
    {
        public MetaVaccinationDesoDto Meta {  get; set; }
        public List<PatientVaccinationDto> Patients { get; set; }
    }
}
