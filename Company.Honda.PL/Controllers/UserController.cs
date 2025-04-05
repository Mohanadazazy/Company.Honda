using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Company.Honda.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Honda.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    UserName = U.UserName,
                    Roles = _userManager.GetRolesAsync(U).Result
                });
            }
            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    UserName = U.UserName,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).Where(U => U.UserName.ToLower().Contains(SearchInput.ToLower()));
            }

            // Dictionary   : 3 Properties
            // 1. ViewData  : Transfer Extra Information From Controller To View
            ViewData["Message"] = "Hello From ViewData";
            // 2. ViewBag   : Transfer Extra Information From Controller To View
            //ViewBag.Message = "Hello Form ViewBag";

            return View(users);
        }

        

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                
                if (id == model.Id)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null) return BadRequest("Invalid Id");


                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.Id = id;
                    
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                        return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest("Wrong Id");
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound("User Not Found");
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}