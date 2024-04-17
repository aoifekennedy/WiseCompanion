using Microsoft.Maui.Controls;
using ServiceReference1;

namespace WiseCompanion
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            entryEmail.Focus();
        }

        private async void SignInBtn(object sender, EventArgs e)
        {
            var entrypassword = entryPassword.Text;
            var entryemail = entryEmail.Text;

            if (string.IsNullOrWhiteSpace(entrypassword) || string.IsNullOrWhiteSpace(entryemail))
            {
                await DisplayAlert("Error", "Please fill in every entry!", "OK");
                return;
            }

            try
            {
                using KentapAFEClient client = new KentapAFEClient(KentapAFEClient.EndpointConfiguration.BasicHttpsBinding_IKentapAFE);

                var result = await client.LoginAsync(entryemail, entrypassword);
                client.Close();
                WiseCompanion.Global.AdminYN = result;
                WiseCompanion.Global.EmailAddress = entryemail;
                

               var homePage = new HomePage();
                await Navigation.PushModalAsync(homePage);  

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error occurred while attempting to log in: " + ex.Message, "OK");
            }
        }


        private async void RedirectSignUpBtn(object sender, EventArgs e)
        {

            var signUpPage = new SignUpPage();
            await Navigation.PushModalAsync(signUpPage);
        }

        private void TextBoxEmail_Completed(object sender, EventArgs e)

        {

            entryPassword.Focus();

        }
        private void TextBoxPassword_Completed(object sender, EventArgs e)

        {

            SignInButton.Focus();

        }

    }


}