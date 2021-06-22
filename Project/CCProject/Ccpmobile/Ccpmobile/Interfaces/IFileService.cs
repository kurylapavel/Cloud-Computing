using System;
using System.Collections.Generic;
using System.Text;

namespace Ccpmobile.Interfaces
{
    public interface IFileService
    {
        string GetRootPath();
        void CreateBasicDirectories();
    }
}
