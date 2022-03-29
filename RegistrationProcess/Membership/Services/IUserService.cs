using Membership.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Services
{
    public interface IUserService
    {
        public (IList<ApplicationUser> records, int total, int totalDisplay) GetAll(int pageIndex, int pageSize, string searchText, string sortText);
        public (IList<ApplicationUser> records, int total, int totalDisplay) GetAllAdmin(int pageIndex, int pageSize, string searchText, string sortText);
        public (IList<ApplicationUser> records, int total, int totalDisplay) GetAllUser(int pageIndex, int pageSize, string searchText, string sortText);
        public ApplicationUser GetById(Guid id);
        public Task<Guid> Add(ApplicationUser user, Guid userRoleId, string password);
        public Task<Guid> Add(ApplicationUser user, string userRoleName, string password);
        public Task<Guid> Update(ApplicationUser user, Guid userRoleId);
        public Task<Guid> Update(ApplicationUser user);
        public Task<bool> ChangePassword(Guid id, string currentPassword, string newPassword);
    }
}
