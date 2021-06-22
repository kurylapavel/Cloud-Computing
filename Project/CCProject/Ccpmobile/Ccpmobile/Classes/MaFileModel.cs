using System;
using System.Collections.Generic;
using System.Text;

namespace Ccpmobile.Classes
{
    public class MaFileModel
    {
        public int status { get; set; }
        public string shared_secret { get; set; }
        public string serial_number { get; set; }
        public string revocation_code { get; set; }
        public string uri { get; set; }
        public string server_time { get; set; }
        public string account_name { get; set; }
        public string token_gid { get; set; }
        public string identity_secret { get; set; }
        public string secret_1 { get; set; }
        public string device_id { get; set; }
        public bool fully_enrolled { get; set; }
        public string account_password { get; set; }
        public Session Session { get; set; }
    }
}
