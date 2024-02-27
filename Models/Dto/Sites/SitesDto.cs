namespace DSU24_Grupp5.Models.Dto.Sites
{
    public class SitesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalNumber { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
