using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Ccpmobile.Classes
{
    public class Localization
    {
        public static async Task<Location> GetMyLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                return location;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
