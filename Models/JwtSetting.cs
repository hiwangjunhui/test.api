using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Authentication.Models
{
    public class JwtSetting
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan ExpireTimeSpan { get; set; }
    }
}
