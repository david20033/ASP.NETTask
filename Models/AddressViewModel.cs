namespace ASP.NETTask.Models
{
    public class AddressViewModel
    {
        public string Street { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
