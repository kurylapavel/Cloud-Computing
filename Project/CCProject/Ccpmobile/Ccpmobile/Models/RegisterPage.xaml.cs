using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ccpmobile.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            btnRegister.Clicked += BtnRegister_Clicked;
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;
            string confirmPass = txtPasswordConfirm.Text;

            if( password != confirmPass)
            {
                await DisplayAlert("Error", "Passwords is not the same!", "Ok!");
                return;
            }
            else if (string.IsNullOrWhiteSpace(login))
            {
                await DisplayAlert("Error", "Login is empty!", "Ok!");
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Password is empty!", "Ok!");
                return;
            }

            string html = string.Empty;
            string url = $@"https://ccprojectfunction.azurewebsites.net/api/CCProjecRegisterTrigger?code=zBr2bMX2LHiTKPTri9JR09aE8q7bhHxAtAU53CWLWojwwGv7tVFYYQ==&log={login}&pass={password}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if (Convert.ToInt32(html) == -1)
            {
                await DisplayAlert("Error", "Something went wrong. Try again later", "Ok!");
                return;
            }
            else if (Convert.ToInt32(html) == 0)
            {
                await DisplayAlert("Error", "This login is already taken!", "Ok!");
                return;
            }


            await Navigation.PushAsync(new LoginPage());

        }
    }
}