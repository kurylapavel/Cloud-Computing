using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var userName = txtLogin.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                await DisplayAlert("Validation errors", "The username is required", "Ok");
                return;
            }

            await Navigation.PushAsync(new ChatPage(userName));
        }
    }
}
