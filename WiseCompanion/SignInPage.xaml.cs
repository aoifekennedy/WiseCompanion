using Microsoft.Maui.Controls;

namespace WiseCompanion
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private async void SignInBtn(object sender, EventArgs e)
        {
            var entrypassword = entryPassword.Text;
            var entryemail = entryEmail.Text;

            if (string.IsNullOrWhiteSpace(entrypassword) ||
               string.IsNullOrWhiteSpace(entryemail))
            {
                await DisplayAlert("Error", "Please fill in every entry!", "OK");
                return;
            }

            var homePage = new HomePage();
            await Navigation.PushModalAsync(homePage);
        }

        private async void RedirectSignUpBtn(object sender, EventArgs e)
        {

            var signUpPage = new SignUpPage();
            await Navigation.PushModalAsync(signUpPage);
        }
    }
}