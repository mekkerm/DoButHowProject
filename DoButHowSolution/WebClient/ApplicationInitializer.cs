
using Dbh.Model.EF.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

public interface IDatabaseInitializer
{
    int Order { get; }

    void Seed();
}

public class AddDefaultRolesDatabaseInititalizer : IDatabaseInitializer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddDefaultRolesDatabaseInititalizer(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public int Order { get; } = 1;

    public void Seed()
    {
        List<IdentityRole> theroles = null;
        using (_roleManager)
        {
            theroles = _roleManager.Roles.ToList();
            if (!_roleManager.Roles.Any())
            {

                var roles = new[]
                { "Admin", "Moderator", "User" };

                foreach (var role in roles)
                {
                    var result = _roleManager.CreateAsync(new IdentityRole { Name = role }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Error creating roles.");
                    }
                }
            }
        }
        using (_userManager)
        {
            var poweruser = new ApplicationUser
            {
                UserName = "admin@email.com",
                Email = "admin@email.com"
            };
            string UserPassword = "admin@email.com";

            var _user = _userManager.FindByEmailAsync(poweruser.Email);

            if (_user.Result == null)
            {
                var createPowerUser = _userManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Result.Succeeded)
                {
                    var newuser = _userManager.Users.Where(x => x.Email == poweruser.Email).FirstOrDefault();
                    //here we tie the new user to the "Admin" role 
                    var res = _userManager.AddToRoleAsync(newuser, "Admin");
                    if (res.Result.Succeeded)
                    {
                        Console.WriteLine("User role assigned");
                    }

                }
            }
        }



    }
}

public class CompositeDatabaseInitializer : IDatabaseInitializer
{
    private readonly IEnumerable<IDatabaseInitializer> _databaseInitializers;

    public CompositeDatabaseInitializer(IEnumerable<IDatabaseInitializer> databaseInitializers)
    {
        _databaseInitializers = databaseInitializers;
    }

    public int Order { get; } = 0;

    public void Seed()
    {
        foreach (var databaseInitializer in _databaseInitializers.OrderBy(initializer => initializer.Order))
        {
            databaseInitializer.Seed();
        }

    }
}