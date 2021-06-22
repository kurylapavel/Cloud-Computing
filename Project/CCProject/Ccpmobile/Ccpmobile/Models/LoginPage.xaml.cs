using Ccpmobile.Interfaces;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            btnSignIn.Clicked += BtnSignIn_Clicked;
            btnRegister.Clicked += BtnRegister_Clicked;
            
        }

        //private void BtnFile_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IFileService>().CreateBasicDirectories();
        //    DisplayAlert("hi", "File sozdan","ok");
        //}

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                await DisplayAlert("Error", "Login is empty!","Ok!");
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Password is empty!", "Ok!");
                return;
            }

            string html = string.Empty;
            string url = $@"https://ccprojectfunction.azurewebsites.net/api/CCProjectTrigger?code=KFo1KZcurwm6GnvYe7g5o7KEVzIf2hA60m2HMZue0Q7U05aOxIYsGQ==&log={login}&pass={password}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if(Convert.ToInt32(html) == -1)
            {
                await DisplayAlert("Error", "Wrong login or password", "Ok!");
                return;
            }



            //File.WriteAllText($"{Application.con}helloword.txt", "hello word!");
            
            await Navigation.PushAsync(new MainPage(html));
        }
    }
}