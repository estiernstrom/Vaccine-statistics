
using DSU24_Grupp5.Infrastructure;
using DSU24_Grupp5.Models.Dto.DeSO;
using System.Text.Json;

namespace DSU24_Grupp5.Models.Dto.VaccinationDeSOCode
{
    public class DesoCodeDto
    {
        public DesoCodeDto(List<VaccinationDesoDto> vaccinationDesoDtos)
        {
            VaccinationDesoDtos = vaccinationDesoDtos;
        }

        public List<VaccinationDesoDto> VaccinationDesoDtos { get; set; } = [];

     
    }
}
