namespace WiseCompanion;

public partial class ActivitiesPage : ContentPage
{
	public ActivitiesPage()
	{
		InitializeComponent();
	}

    private async Task<Location> GetCurrentLocationAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var currentLocation = await Geolocation.GetLocationAsync(request);
            return currentLocation;
        }
        catch (Exception ex)
        {
            // Handle any exception (location services disabled, etc.)
            Console.WriteLine(ex.ToString());
            return null;
        }
    }


    private async void FindActivitiesNearMeClicked(object sender, EventArgs e)
    {
        var currentLocation = await GetCurrentLocationAsync();
        if (currentLocation != null)
        {
            string query = Uri.EscapeDataString("Senior Activities Near Me");

            // Construct the Google Maps URL
            string mapsUrl = $"https://www.google.com/maps/search/?api=1&query={query}&center={currentLocation.Latitude},{currentLocation.Longitude}";

            try
            {
                await Launcher.OpenAsync(new Uri(mapsUrl));
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., no available app to handle the URI)
                Console.WriteLine($"Could not open Google Maps: {ex.Message}");
            }
        }
        else
        {
            // Inform the user that the location could not be determined
            Console.WriteLine("Current location is unavailable.");
        }
    }

}