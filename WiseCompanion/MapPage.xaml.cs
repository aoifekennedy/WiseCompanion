using Microsoft.Maui.Devices;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel;
using ServiceReference1;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;
using System.Net.Mail;
using System.Xml;

namespace WiseCompanion;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();

     }

    private async void OpenInGoogleMapsClicked(object sender, EventArgs e)
    {
        var emailAddress = WiseCompanion.Global.EmailAddress;

        try
        {

            using KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);

            var XMLResult = await client.FindMyCarAsync(emailAddress);
            client.Close();

            XmlNode longitudeNode = XMLResult.SelectSingleNode("Longitude");
            XmlNode latitudeNode = XMLResult.SelectSingleNode("Latitude");

            string Longitude = longitudeNode.InnerXml;
            string Latitude = latitudeNode.InnerXml;

            if (longitudeNode != null && latitudeNode != null)
            {
                double destinationLongitude = Convert.ToDouble(Longitude);
                double destinationLatitude = Convert.ToDouble(Latitude);

                // Construct the Google Maps URL
                string googleMapsUrl = $"https://www.google.com/maps/dir/?api=1&destination={destinationLongitude}, {destinationLatitude}&travelmode=driving";
                await Launcher.Default.OpenAsync(new Uri(googleMapsUrl));
            }
            else
            {
                await DisplayAlert("Error", "Longitude or Latitude not found", "OK");
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to open Google Maps: " + ex.Message, "OK");
        }
    }

    private async void AddCarLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Denied", "Location permission is not granted", "OK");
                    return;
                }
            }

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");

                var emailAddress = WiseCompanion.Global.EmailAddress;
                KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
                await client.SaveCurrentLocationAsync(emailAddress, location.Latitude.ToString(), location.Longitude.ToString());
                client.Close();

                AddCarLocationButton.IsVisible = false;
                RemoveCarLocationButton.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Location Unavailable", "Unable to obtain location.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error adding car location: {ex.Message}", "OK");
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateLocationButtonAsync();
    }

    private async Task UpdateLocationButtonAsync()
    {
        var emailAddress = WiseCompanion.Global.EmailAddress;
        KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
        bool hasActiveLocation = await client.IsThereACurrentActiveLocationAsync(emailAddress);
        client.Close();

        Device.BeginInvokeOnMainThread(() =>
        {
            AddCarLocationButton.IsVisible = !hasActiveLocation;
            RemoveCarLocationButton.IsVisible = hasActiveLocation;
        });
    }



    private async void RemoveCarLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var emailAddress = WiseCompanion.Global.EmailAddress;
            KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);

            var result = await client.RemoveCurrentLocationAsync(emailAddress);
            client.Close();

            if (result)
            {
                AddCarLocationButton.IsVisible = true;
                RemoveCarLocationButton.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Error", "Failed to remove car location.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error removing car location: {ex.Message}", "OK");
        }
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        var homePage = new HomePage();
        await Navigation.PushModalAsync(homePage);
    }

}







