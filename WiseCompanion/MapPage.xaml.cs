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

            KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);

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
                string googleMapsUrl = $"https://www.google.com/maps/dir/?api=1&destination={destinationLatitude},{destinationLongitude}&travelmode=driving";
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

                await DisplayAlert("Success", "Car location added successfully.", "OK");
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




    }

