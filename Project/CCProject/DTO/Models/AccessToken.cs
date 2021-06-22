using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models
{
    public class AccessToken
    {
        public long ExpriesIn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Token { get; set; }

    }
}
