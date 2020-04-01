using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
	public class Department:BaseEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string RoleName { get; set; }
		
	}
}
