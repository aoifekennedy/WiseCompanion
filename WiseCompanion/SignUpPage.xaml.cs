using System;
using Microsoft.Maui.Controls;
using ServiceReference1;

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
            var name = entryName.Text;
            var email = entryEmail.Text;
            var password = entryPassword.Text;
            var sosPhoneNo = sosPhoneNumber.Text;
            var adminEmailText = adminEmail.Text; 
            var adminPasswordText = adminPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sosPhoneNo))
            {
                await DisplayAlert("Error", "Please fill in every entry!", "OK");
                return;
            }

            try
            {
                KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);

                var result = await client.SignUpAsync(email, password, name, sosPhoneNo, adminEmailText, adminPasswordText);
                client.Close();


                    var signInPage = new SignInPage();
                    await Navigation.PushModalAsync(signInPage);
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }



        private async void RedirectSignInBtn(object sender, EventArgs e)
        {

            var signInPage = new SignInPage();
            await Navigation.PushModalAsync(signInPage);
        }
    }
}
