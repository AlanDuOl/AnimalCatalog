using AnimalCatalogSqLite.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AnimalCatalogSqLite.ViewModels;
using System.Security.Claims;

namespace AnimalCatalogSqLite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CatalogContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(CatalogContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IdentityUser GetUserByEmail(string email)
        {
            IdentityUser user = new IdentityUser();
            try
            {
                user = _userManager.FindByEmailAsync(email).Result;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return user;
        }

        public IdentityUser GetUser(ClaimsPrincipal claimsUser)
        {
            IdentityUser user = new IdentityUser();
            try
            {
                user = _userManager.GetUserAsync(claimsUser).Result;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return user;
        }

        public List<ViewUserList> GetUsers()
        {
            List<ViewUserList> users = new List<ViewUserList>();
            try
            {
                var identityUsers = _userManager.Users;
                foreach (IdentityUser user in identityUsers)
                {
                    List<string> roleNamesList = GetRolesByUser(user);
                    string roleNames = string.Join(",", roleNamesList);

                    ViewUserList viewUser = new ViewUserList
                    {
                        Email = user.Email,
                        Name = user.UserName,
                        Roles = roleNames
                    };
                    users.Add(viewUser);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return users;
        }

        public List<string> GetRolesByUser(IdentityUser user)
        {
            List<string> userRoles = new List<string>();
            try
            {
                var result = _userManager.GetRolesAsync(user).Result;
                userRoles = (List<string>)result;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return userRoles;
        }

        public bool RemoveUser(string email)
        {
            bool userRemoved = false;
            try
            {
                IdentityUser user = _dbContext.Users.SingleOrDefault(val => val.Email == email);
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    userRemoved = true;
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return userRemoved;
        }

        public bool UpdateUserRoles(ViewUserRoles user)
        {
            bool rolesUpdated = false;
            try
            {
                IdentityUser identityUser = _userManager.FindByEmailAsync(user.Email).Result;

                if (identityUser != null)
                {
                    Dictionary<string, bool> roles = new Dictionary<string, bool>() {
                        { "Admin", user.Admin }, { "User", user.User }, { "Manager", user.Manager }
                    };

                    foreach (var role in roles)
                    {
                        bool userHasRole = _userManager.IsInRoleAsync(identityUser, role.Key).Result;
                        // Add role if user does not have it
                        if (role.Value == true)
                        {
                            if (userHasRole == false)
                            {
                                var roleResult = _userManager.AddToRoleAsync(identityUser, role.Key).Result;
                                if (!roleResult.Succeeded)
                                {
                                    Debug.WriteLine("Error: could not add user to role");
                                }
                                rolesUpdated = true;
                            }
                        }
                        // Remove role if user has it
                        else if (role.Value == false)
                        {
                            if (userHasRole == true)
                            {
                                var roleResult = _userManager.RemoveFromRoleAsync(identityUser, role.Key).Result;
                                if (!roleResult.Succeeded)
                                {
                                    Debug.WriteLine("Error: could not remove user from role");
                                }
                                rolesUpdated = true;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return rolesUpdated;
        }

        public bool UpdateCommonUserData(ViewUserList user)
        {
            bool dataUpdated = false;
            try
            {
                IdentityUser identityUser = _userManager.FindByEmailAsync(user.Email).Result;
                if (identityUser != null)
                {
                    identityUser.Email = user.Email;
                    identityUser.UserName = user.Name;
                    _dbContext.Users.Update(identityUser);
                    _dbContext.SaveChanges();
                    dataUpdated = true;
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return dataUpdated;
        }
    }
}
