﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid UserId {get;set;}
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}