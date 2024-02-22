using System;
using Microsoft.Maui.Controls;

namespace WiseCompanion
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void SignUpBtn(object sender, EventArgs e)
        {
            var entryUsername = entryName.Text;
            var entrypassword = entryPassword.Text;
            var entryemail = entryEmail.Text;
            var sosphoneNumber = sosPhoneNumber.Text;

            if (string.IsNullOrWhiteSpace(entryUsername) ||
                string.IsNullOrWhiteSpace(entrypassword) ||
                string.IsNullOrWhiteSpace(entryemail) ||
                string.IsNullOrWhiteSpace(sosphoneNumber))
            {
                await DisplayAlert("Error", "Please fill in every entry!", "OK");
                return;
            }

            var signInPage = new SignInPage();
            await Navigation.PushModalAsync(signInPage);
        }


        private async void RedirectSignInBtn(object sender, EventArgs e)
        {

            var signInPage = new SignInPage();
            await Navigation.PushModalAsync(signInPage);
        }
    }
}
