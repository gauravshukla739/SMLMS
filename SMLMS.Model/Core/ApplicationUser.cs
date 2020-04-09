using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class ApplicationUser : IdentityUser<Guid>
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime DateOfJoin { get; set; }
		public DateTime DateOfAppointment { get; set; }
		public DateTime DateOfLeave { get; set; }
		public string Address { get; set; }
		public bool? IsDeleted { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public string IsDeletedBy { get; set; }
		public byte[] Image { get; set; }

	}
}
