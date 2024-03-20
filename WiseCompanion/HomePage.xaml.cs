namespace WiseCompanion;
using ServiceReference1;
using System.Net.Mail;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        sosBtn.Clicked += OnSOSButtonClicked;
    
    }

    private async void LogOutBtn(object sender, EventArgs e)
    {
        var landingPage = new LandingPage();
        await Navigation.PushModalAsync(landingPage);
    }

    private async void ImgBtn(object sender, EventArgs e)
    {
        var landingPage = new LandingPage();
        await Navigation.PushModalAsync(landingPage);
    }

    private async void FindMyCarBtn(object sender, EventArgs e)
    {
        var mapPage = new MapPage();
        await Navigation.PushModalAsync(mapPage);
    }

    private async void OnSOSButtonClicked(object sender, EventArgs e)
    {
        try
        {
            string userEmail = WiseCompanion.Global.EmailAddress;

            KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);
            {
                string phoneNumber = await client.GetSOSAsync(userEmail);

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    PhoneDialer.Open(phoneNumber);
                }
                else
                {
                    await DisplayAlert("Error", "No SOS phone number found for the user.", "OK");
                }
            }
        }
        catch (ArgumentNullException)
        {
            await DisplayAlert("Error", "An essential argument was null.", "OK");
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Error", "This feature is not supported on your device.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "An error occurred: " + ex.Message, "OK");
        }
    }



    private async void ActivitiesBtn(object sender, EventArgs e)
    {
        var activitiesPage = new ActivitiesPage();
        await Navigation.PushModalAsync(activitiesPage);
    }

    private async void MedicationBtn(object sender, EventArgs e)
    {
        var medicationPage = new MedicationPage();
        await Navigation.PushModalAsync(medicationPage);
    }
}
