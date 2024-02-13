namespace WiseCompanion;

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

    private void OnSOSButtonClicked(object sender, EventArgs e)
    {
        try
        {
            string phoneNumber = "0851307283";
            PhoneDialer.Open(phoneNumber);
        }
        catch (ArgumentNullException)
        {
        }
        catch (FeatureNotSupportedException)
        {
        }
        catch (Exception)
        {
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
