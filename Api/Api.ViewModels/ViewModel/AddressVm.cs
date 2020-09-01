namespace Api.ViewModels.ViewModel
{
    public class AddressVm
    {
        public string place { get; set; }
        public string road { get; set; }
        public string houseNumber { get; set; }
        public string zipcode { get; set; }
        public LatLngVm latLng { get; set; }
    }
}
