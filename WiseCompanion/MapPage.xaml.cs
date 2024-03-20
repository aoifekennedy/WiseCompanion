using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.ApplicationModel;
using ServiceReference1;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Xml.Linq;
using System.Net.Mail;
using System.Xml;
//using Windows.Data.Xml.Dom;

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
                await DisplayAlert("Error", "Longitude or Latitude not found in XML.", "OK");
            }
        }

        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to open Google Maps: " + ex.Message, "OK");
        }
    }

}

