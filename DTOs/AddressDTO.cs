namespace ASP.NETTask.DTOs
{
    public class AddressDTO
    {
        public string Street { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public GeoDTO Geo { get; set; } = new GeoDTO();
    }
}
