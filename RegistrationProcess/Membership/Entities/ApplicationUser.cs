using Membership.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string LastPassword { get; set; }
        public EnumRoles RoleStatus { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActiveRole { get; set; }
        public bool IsBlockedRole { get; set; }
        public bool IsDeletedRole { get; set; }
        public IList<ApplicationUserRole> UserRoles { get; set; }
        public ApplicationUser()
                    : base()
        {
            this.IsActiveRole = true;
            this.IsDeletedRole = false;
            this.IsBlockedRole = false;
            this.UserRoles = new List<ApplicationUserRole>(); 
        }

        internal ApplicationUser(string userName)
            : base(userName)
        {
            this.IsActiveRole = true;
            this.IsDeletedRole = false;
            this.IsBlockedRole = false;
            this.UserRoles = new List<ApplicationUserRole>();
        }

        public ApplicationUser(string userName, string mobileNumber, string email)
            : base(userName)
        {
            this.Email = email;
        }

        public ApplicationUser(string userName, string fullName, string mobileNumber, string email)
            : this(userName, mobileNumber, email)
        {
            this.FullName = fullName;
        }

        public ApplicationUser(string userName, string fullName, string mobileNumber, string phone, string email)
            : this(userName, fullName, mobileNumber, email)
        {
            this.PhoneNumber = phone;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            var hashCode = -582740416;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}
