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
            // Number was null or white space
        }
        catch (FeatureNotSupportedException)
        {
            // Phone Dialer is not supported on this device.
        }
        catch (Exception)
        {
            // Other error has occurred.
        }
    }
}
