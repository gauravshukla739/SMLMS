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
        public string RoleName { get; set; }
        public bool RememberMe { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public DateTime DateOfJoin { get; set; }
		public DateTime DateOfAppointment { get; set; }
		public DateTime DateOfLeave { get; set; }
		public string DepartmentId { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
	}
}
