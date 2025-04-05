using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Company.Honda.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Honda.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> role;
            if (string.IsNullOrEmpty(SearchInput))
            {
                role = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                   Id = U.Id,
                   Name = U.Name
                });
            }
            else
            {
                role = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name,
                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            // Dictionary   : 3 Properties
            // 1. ViewData  : Transfer Extra Information From Controller To View
            ViewData["Message"] = "Hello From ViewData";
            // 2. ViewBag   : Transfer Extra Information From Controller To View
            //ViewBag.Message = "Hello Form ViewBag";

            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByNameAsync(model.Name);
                if(role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {

                if (id == model.Id)
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null) return BadRequest("Invalid Id");


                    role.Name = model.Name;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest("Wrong Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound("User Not Found");
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            ViewData["roleId"] = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return BadRequest();

            var usersInRole = new List<UserToRoleDto>();

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UserToRoleDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UserToRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return BadRequest();

            if(ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.Id);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction("Edit", new { id = role.Id });
            }
            return View(users);
        }
    }
}
