using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Helper.AppSetting
{
        public class SmtpDetails
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public bool UseSsl { get; set; }
            public string Password { get; set; }
            public string FromEmail { get; set; }

        }
}
