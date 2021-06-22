using Ccpmobile.Interfaces;
using Ccpmobile.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Ccpmobile.Classes;
using Ccpmobile.SteamAuth.dll;
using System.Net;


namespace Ccpmobile.Models
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public string userId;
        public MafilePageViewModel mafilePageViewModel = new MafilePageViewModel();
        public MainPage(string userId)
        {

            
            InitializeComponent();
            DependencyService.Get<IFileService>().CreateBasicDirectories();

            this.userId = userId;
            swSafeMode.IsToggled = Preferences.Get("SwitchToggled", false);

            BindingContext = new MafilePageViewModel();
            CreateMafileListView();


            btnAddMafile.Clicked += BtnAddMafile_Clicked;
            btnRemoveMafile.Clicked += BtnRemoveMafile_Clicked;
            swSafeMode.Toggled += SwSafeMode_Toggled;
            imgCopy.Clicked += ImgCopy_Clicked;  
           
        }

        private async void ImgCopy_Clicked(object sender, EventArgs e)
        {
            if (lbCode.Text == "*****")
            {
                return;
            }
                
            await Clipboard.SetTextAsync(lbCode.Text);
            DependencyService.Get<IToast>().Show("Copied!");
        }

        private void BtnRemoveMafile_Clicked(object sender, EventArgs e)
        {
            MaFileModel model;
            string maFileName = String.Empty;
            try
            {
                model = (MaFileModel)listview1.SelectedItem;
                maFileName = $"{model.Session.SteamID}.maFile";
            }
            catch(Exception ex)
            {
                return;
            }
            

            File.Delete(Path.Combine($"{DependencyService.Get<IFileService>().GetRootPath()}/maFiles", maFileName));
            lbCode.Text = "*****";
            CreateMafileListView();
        }

        private async void SwSafeMode_Toggled(object sender, ToggledEventArgs e)
        {
            var location = await Localization.GetMyLocation();
            var latitude = location.Latitude.ToString().Replace(',','.');
            var longitude = location.Longitude.ToString().Replace(',', '.');

            Preferences.Set("SwitchToggled", e.Value);
            if (e.Value)
            {
                string html = string.Empty;
                string url = $@"https://ccprojectfunction.azurewebsites.net/api/SetTrustGeoLocationTrigger?code=S/7Sgc1wXBaPwdBlpm8D/Pc3x/LlFSATFR0bPaCJFkU6H16aGWHfAQ==&Id={userId}&latitude={latitude}&longitude={longitude}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                }

                if (Convert.ToInt32(html) == -1 || Convert.ToInt32(html) == 0)
                {
                    await DisplayAlert("Error", "Can't import your geolocation", "Ok!");
                    return;
                }
                await DisplayAlert("OK", "Successfully imported your geolocation", "Ok!");
            }
            else
            {
                string html = string.Empty;
                string url = $@"https://ccprojectfunction.azurewebsites.net/api/CheckTrustedGeoLocationTrigger?code=Cis3NxXq5HcOMxqayucPXmWjGla6fcJAGXvcHbe9cRJ7TvOh1LJmdQ==&Id={userId}&latitude={latitude}&longitude={longitude}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                }
                

                if (Convert.ToInt32(html) == 0)
                {
                    await DisplayAlert("Bad Location", "You must be in trusted location", "Ok!");
                    swSafeMode.Toggled -= SwSafeMode_Toggled;
                    swSafeMode.IsToggled = true;
                    swSafeMode.Toggled += SwSafeMode_Toggled;
                    return;
                }

                await DisplayAlert("OK", "Trust mode is disabled", "Ok!");

            }
            

        }

        private void CreateMafileListView()
        {
            if(mafilePageViewModel.MaFilesList.Count > 0)
            {
                mafilePageViewModel.MaFilesList.Clear();
            }

            DirectoryInfo dir = new DirectoryInfo($"{DependencyService.Get<IFileService>().GetRootPath()}/maFiles");
            foreach (var maf in dir.GetFiles())
            {
                var text = File.ReadAllText(maf.FullName);
                var json = JsonConvert.DeserializeObject<MaFileModel>(text);
                mafilePageViewModel.AddItemToList(json);
            }
            listview1.ItemsSource = mafilePageViewModel.MaFilesList;

        }
        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            if (swSafeMode.IsToggled)
            {
                var location = await Localization.GetMyLocation();
                var latitude = location.Latitude.ToString().Replace(',', '.');
                var longitude = location.Longitude.ToString().Replace(',', '.');

                string html = string.Empty;
                string url = $@"https://ccprojectfunction.azurewebsites.net/api/CheckTrustedGeoLocationTrigger?code=Cis3NxXq5HcOMxqayucPXmWjGla6fcJAGXvcHbe9cRJ7TvOh1LJmdQ==&Id={userId}&latitude={latitude}&longitude={longitude}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                }


                if (Convert.ToInt32(html) == 0)
                {
                    lbCode.Text = "*****";
                    await DisplayAlert("Bad Location", "You must be in trusted location", "Ok!");
                    return;
                }
            }
            
            var model = (MaFileModel)e.Item;
            SteamGuardAccount acc = new SteamGuardAccount();
            acc.SharedSecret = model.shared_secret;

            var code = acc.GenerateSteamGuardCode();
            lbCode.Text = code;
        }
        private async void BtnAddMafile_Clicked(object sender, EventArgs e)
        {


            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "application/maFile" } },
            });


            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                
            });

            if(pickResult != null)
            {
                foreach(var mafile in pickResult)
                {
                    try
                    {
                        File.Copy(mafile.FullPath, Path.Combine($"{DependencyService.Get<IFileService>().GetRootPath()}/maFiles", mafile.FileName));
                    }catch(Exception ex)
                    {
                        if(ex.Message.Contains("File already exists."))
                        {
                            continue;
                        }
                        else
                        {
                            await DisplayAlert("Error", "Something went wrong :(", "Ok");
                        }
                    }
                    
                }
                
                
            }
            CreateMafileListView();
        }
    }
}