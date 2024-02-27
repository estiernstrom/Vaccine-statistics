namespace DSU24_Grupp5.Models.ViewModel
{
    public class IndexFilterViewModel
    {
        public int DoseNumber { get; set; } = 1;
        public string? VaccineName { get; set; } 
        public string? Gender { get; set; }
        public string? AgeInterval { get; set; } = "15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80";
        public string? VaccinationStation { get; set;}
        public string? BatchNumber { get; set; } 
        public List<string> DesoCodes { get; set; } = ["C"];
        public string? DesoCode1 { get; set; } = "2380C1200";
        public string? DesoCode2 { get; set; } = "2380C1220";
        public string? Manufacturer { get; set; }
    }
}
