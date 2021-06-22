using System;
using System.Collections.Generic;
using System.Text;

namespace Ccpmobile.Classes
{
    public class Session
    {
        public string SessionID { get; set; }
        public string steamLogin { get; set; }
        public string steamLoginSecure { get; set; }
        public object WebCookie { get; set; }
        public string OAuthToken { get; set; }
        public string SteamID { get; set; }
    }
}
