using Microsoft.AspNetCore.Identity;

namespace FarmManagement.UI.Web;

public class IdentitySeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentitySeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Seed()
    {
        var userEmails = new[] { "lars@kdg.be", "anna@kdg.be", "bob@kdg.be", "chef@kdg.be", "test@kdg.be" };

        const string admin = "Admin";
        const string user = "User";
        
        var adminRole = new IdentityRole(admin);
        _roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
        
        var userRole = new IdentityRole(user);
        _roleManager.CreateAsync(userRole).GetAwaiter().GetResult();
        
        var adminUser1 = new IdentityUser(userEmails[0]) { Email = userEmails[0], EmailConfirmed = true };
        _userManager.CreateAsync(adminUser1,"Password1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser1, admin);
        
        var adminUser2 = new IdentityUser(userEmails[1]) { Email = userEmails[1], EmailConfirmed = true };
        _userManager.CreateAsync(adminUser2,"Password1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser2, admin);
        
        var user1 = new IdentityUser(userEmails[2]) { Email = userEmails[2], EmailConfirmed = true };
        _userManager.CreateAsync(user1,"Password1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(user1,user);
        
        var user2 = new IdentityUser(userEmails[3]) { Email = userEmails[3], EmailConfirmed = true };
        _userManager.CreateAsync(user2,"Password1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(user2,user);
        
        var user3 = new IdentityUser(userEmails[4]) { Email = userEmails[4], EmailConfirmed = true };
        _userManager.CreateAsync(user3,"Password1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(user3,admin);
        
        
        
        
        
        

    }
}