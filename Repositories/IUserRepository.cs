using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCatalogSqLite.ViewModels;
using System.Security.Claims;

namespace AnimalCatalogSqLite.Repositories
{
    public interface IUserRepository
    {
        List<ViewUserList> GetUsers();
        IdentityUser GetUserByEmail(string email);
        bool UpdateUserRoles(ViewUserRoles user);
        bool RemoveUser(string email);
        List<string> GetRolesByUser(IdentityUser user);
        IdentityUser GetUser(ClaimsPrincipal claimsUser);
        bool UpdateCommonUserData(ViewUserList user);
    }
}
