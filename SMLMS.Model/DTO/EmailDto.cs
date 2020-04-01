using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
    public class EmailDto
    {
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
