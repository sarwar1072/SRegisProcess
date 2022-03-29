using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Membership.Data;
using Membership.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly ICurrentUserService _currentUserService;

        public UserService(
            UserManager userManager,
            RoleManager roleManager,
            ICurrentUserService currentUserService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
        }

        public (IList<ApplicationUser> records,int total,int totalDisplay) GetAll(int pageIndex,int pageSize,string searchText,string sortText)
        {
            var users = new List<ApplicationUser>();
            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = x => x.FullName,
                ["userName"] = x => x.UserName,
                ["email"] = x => x.Email
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            var total = query.CountAsync();

            query = query.Where(x => !x.IsDeletedRole &&
                x.RoleStatus != EnumRoles.SuperAdmin &&
                (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
                x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

            var totalDisplay = query.CountAsync();
            var result = query.AsNoTracking().ToList();
            return (result, 0, 0);
        }
        public (IList<ApplicationUser> records, int total, int totalDisplay) GetAllAdmin(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var users = new List<ApplicationUser>();
            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = x => x.FullName,
                ["userName"] = x => x.UserName,
                ["email"] = x => x.Email
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            var total = query.CountAsync();

            query = query.Where(x => !x.IsDeletedRole &&
                x.RoleStatus != EnumRoles.SuperAdmin &&
                (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
                x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

            var totalDisplay = query.CountAsync();
            var result = query.AsNoTracking().ToList();
            return (result, 0, 0);
        }
        public (IList<ApplicationUser> records, int total, int totalDisplay) GetAllUser(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var users = new List<ApplicationUser>();
            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["fullName"] = x => x.FullName,
                ["userName"] = x => x.UserName,
                ["email"] = x => x.Email
            };

            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            var total = query.CountAsync();

            query = query.Where(x => !x.IsDeletedRole && x.UserRoles.Any(ur => ur.Role.Name == new RoleNames().User) &&
                x.RoleStatus != EnumRoles.SuperAdmin &&
                (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
                x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

            var totalDisplay = query.CountAsync();
            var result = query.AsNoTracking().ToList();
            return (result, 0, 0);
        }

        public ApplicationUser GetById(Guid id)
        {
            var query = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();

            var user = query.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception(nameof(ApplicationUser));
            }

            return user;
        }
        public async Task<Guid> Add(ApplicationUser entity, Guid userRoleId, string newPassword)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    entity.RoleStatus = EnumRoles.User;
                    entity.CreationTime = DateTime.Now;
                    entity.CreatedBy = _currentUserService.UserId;

                    var userSaveResult = _userManager.CreateAsync(entity, newPassword);

                    if (!userSaveResult.IsCompletedSuccessfully)
                    {
                        throw new Exception("Error!");
                    };

                    var user = await _userManager.FindByNameAsync(entity.UserName);
                    var role = await _roleManager.FindByIdAsync(userRoleId.ToString());

                    if (role == null)
                    {
                        throw new Exception(nameof(Role));
                    }

                    var roleSaveResult = _userManager.AddToRoleAsync(user, role.Name);

                    scope.Complete();

                    return user.Id;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public async Task<Guid> Add(ApplicationUser entity, string userRoleName, string newPassword)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    entity.RoleStatus = EnumRoles.User;
                    entity.CreationTime = DateTime.Now;
                    entity.CreatedBy = _currentUserService.UserId;

                    var userSaveResult = await _userManager.CreateAsync(entity, newPassword);

                    if (!userSaveResult.Succeeded)
                    {
                        throw new Exception();
                    };

                    // Add New User Role
                    var user = await _userManager.FindByNameAsync(entity.UserName);
                    var role = await _roleManager.FindByNameAsync(userRoleName);

                    if (role == null)
                    {
                        throw new Exception();
                    }

                    var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

                    if (!roleSaveResult.Succeeded)
                    {
                        throw new Exception();
                    };

                    scope.Complete();

                    return user.Id;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public async Task<Guid> Update(ApplicationUser entity, Guid UserRoleId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var user = await this._userManager.FindByIdAsync(entity.Id.ToString());

                    user.FullName = entity.FullName;
                    user.UserName = entity.UserName;
                    user.Email = entity.Email;
                    user.PhoneNumber = entity.PhoneNumber;
                    user.CreationTime = DateTime.Now;
                    user.Gender = entity.Gender;
                    user.ImageUrl = entity.ImageUrl ?? user.ImageUrl;
                    user.CreatedBy = _currentUserService.UserId;

                    var userSaveResult = await this._userManager.UpdateAsync(user);

                    if (!userSaveResult.Succeeded)
                    {
                        throw new Exception();
                    };

                    var previousUserRoles = await _userManager.GetRolesAsync(user);
                    if (previousUserRoles.Any())
                    {
                        var roleRemoveResult = await _userManager.RemoveFromRolesAsync(user, previousUserRoles);

                    }
                    var role = await _roleManager.FindByIdAsync(UserRoleId.ToString());

                    if (role == null)
                    {
                        throw new Exception();
                    }

                    var roleSaveResult = await _userManager.AddToRoleAsync(user, role.Name);

                    scope.Complete();

                    return user.Id;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public async Task<Guid> Update(ApplicationUser entity)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var user = await this._userManager.FindByIdAsync(entity.Id.ToString());

                    user.FullName = entity.FullName;
                    user.UserName = entity.UserName;
                    user.Email = entity.Email;
                    user.PhoneNumber = entity.PhoneNumber;
                    user.CreationTime = DateTime.Now;
                    user.Gender = entity.Gender;
                    user.ImageUrl = entity.ImageUrl ?? user.ImageUrl;
                    user.CreatedBy = _currentUserService.UserId;

                    var userSaveResult = await this._userManager.UpdateAsync(user);

                    if (!userSaveResult.Succeeded)
                    {
                        throw new Exception();
                    };

                    scope.Complete();

                    return user.Id;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
        public async Task<string> Delete(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id.ToString());

                    var result = await _userManager.DeleteAsync(user);

                    if (!result.Succeeded)
                    {
                        throw new Exception();
                    };

                    scope.Complete();

                    return user.FullName;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }

        }
        public async Task<bool> ChangePassword(Guid id, string CurrentPassword, string NewPassword)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id.ToString());

                    if (user == null)
                    {
                        throw new Exception();
                    }

                    var oldPassword = user.PasswordHash;
                    var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

                    user.LastPassword = oldPassword;

                    var userSaveResult = await _userManager.UpdateAsync(user);

                    if (!userSaveResult.Succeeded)
                    {
                        throw new Exception();
                    };

                    scope.Complete();

                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }

        }
    }
}
