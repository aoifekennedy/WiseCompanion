using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;

namespace WiseCompanion
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            var signUpPage = new SignUpPage();
            await Navigation.PushModalAsync(signUpPage);
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            var signInPage = new SignInPage();
            await Navigation.PushModalAsync(signInPage);
        }
    }


}