using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swimming_Club_2.Viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimming_Club_2.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AllRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole() { Name = model.RoleName };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        
        [HttpPost]
        //public async Task<IActionResult> DeleteRole(string roleId)
        //{
        //    IdentityRole role = await roleManager.FindByIdAsync(roleId);
        //    var result = await roleManager.DeleteAsync(role);
        //    return View("index");
        //}
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            //build editRoleViewModel, pass it to view
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} can not be found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                RoleId = id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            ViewBag.RoleId = role.Id;
            var editUsersInRoleViewModel = new List<EditUsersInRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userInRoleModel = new EditUsersInRoleViewModel();
                userInRoleModel.UserName = user.UserName;
                userInRoleModel.UserId = user.Id;

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRoleModel.IsSelected = true;
                }
                editUsersInRoleViewModel.Add(userInRoleModel);
            }
            return View(editUsersInRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<EditUsersInRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            for (int i = 0; i < model.Count; i++)
            {
                var identityUser = await userManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected)
                {
                    var result = await userManager.AddToRoleAsync(identityUser, role.Name);
                }
                else
                {
                    var result = await userManager.RemoveFromRoleAsync(identityUser, role.Name);
                }
            }

            EditRoleViewModel editModel = new EditRoleViewModel()
            {
                RoleId = roleId,
                RoleName = role.Name
            };

            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}

    
