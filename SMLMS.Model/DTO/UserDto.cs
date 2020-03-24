using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        public bool RememberMe { get; set; }
    }
}
