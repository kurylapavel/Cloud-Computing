using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ccpmobile.Interfaces;
using Ccpmobile.Droid.Classes;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace Ccpmobile.Droid.Classes
{
    public class FileService : IFileService
    {
        public string GetRootPath()
        {
            return Application.Context.GetExternalFilesDir(null).ToString();
            
        }
        public void CreateBasicDirectories()
        {
            var directoryName = "maFiles";
            var destanation = Path.Combine(GetRootPath(), directoryName);


            Directory.CreateDirectory(destanation);
        }
    }
}