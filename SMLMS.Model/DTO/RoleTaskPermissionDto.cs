using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
	public class RoleTaskPermissionDto
	{
		public Guid Id { get; set; }
		public string TaskName { get; set; }
		public string RoleName { get; set; }
		public string Permission { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		
	}
}
