using Ccpmobile.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ccpmobile.ViewModels
{
    public class MafilePageViewModel
    {
        public ObservableCollection<MaFileModel> MaFilesList { get; set; }

        public MafilePageViewModel()
        {
            MaFilesList = new ObservableCollection<MaFileModel>();
        }
        public void AddItemToList(MaFileModel mafile)
        {
            
            MaFilesList.Add(mafile);

            
        }
    }
}
