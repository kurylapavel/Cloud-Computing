using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Commands
{
    public class CreateAccessToken
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
