using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
	public class UserRole : IdentityUserRole<Guid>
	{
		public string DepartmentId { get; set; }
		public bool? IsDeleted { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public string IsDeletedBy { get; set; }
	}
}
