﻿// WebApplication7.Controllers klasöründeki RoleController.cs

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Utility;

public class RoleController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Bu eylem kullanıcıyı "Admin" rolüne atar
    public async Task<IActionResult> AssignAdminRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var roleName = UserRoles.Role_Admin;

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction("Index", "Home"); // İstenirse başka bir yere yönlendirilebilir.
        }

        return NotFound();
    }

    // Bu eylem tüm kullanıcıları "Patient" rolüne atar, Admin hariç
    public async Task<IActionResult> AssignPatientsRole()
    {
        var roleName = UserRoles.Role_Patient;

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var adminRole = await _roleManager.FindByNameAsync(UserRoles.Role_Admin);

        var usersToAssign = _userManager.Users.Where(u => u.UserName != "b201210001@sakarya.edu.tr").ToList();

        foreach (var user in usersToAssign)
        {
            if (!await _userManager.IsInRoleAsync(user, UserRoles.Role_Patient))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        return RedirectToAction("Index", "Home"); // İstenirse başka bir yere yönlendirilebilir.
    }
}
