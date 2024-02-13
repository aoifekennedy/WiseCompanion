using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel; 

namespace WiseCompanion;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
    }

    private async void OpenInGoogleMapsClicked(object sender, EventArgs e)
    {
        double destinationLatitude = 52.6736;
        double destinationLongitude = -8.5724;

        // Construct the Google Maps URL
        string googleMapsUrl = $"https://www.google.com/maps/dir/?api=1&destination={destinationLatitude},{destinationLongitude}&travelmode=driving";

        try
        {
            await Launcher.Default.OpenAsync(new Uri(googleMapsUrl));
        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Error", "Unable to open Google Maps: " + ex.Message, "OK");
        }
    }
}
