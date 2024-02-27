using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DSU24_Grupp5.Models.Dto.VaccinationCount
{
    public class VaccinationCountDto
    {
        public MetaVaccinationCountDto Meta { get; set; }
        public List<DataVaccinationCountDto> Data { get; set; }
    }
}
