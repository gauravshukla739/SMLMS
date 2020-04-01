using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
    public class ResetPasswordDto
    {       
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string Token { get; set; }
    }
}
