using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Helper.ServiceResponse
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
