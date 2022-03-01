using Eventures.Data;
using Eventures.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;

namespace Eventures.Controllers
{
    public class EventureUsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<EventuresUser>_userManager;

        public EventureUsersController(ApplicationDbContext _context, UserManager<EventuresUser> userManager)
        {
            this.context = _context;
            this._userManager = userManager;
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.Select(u => new UserListingViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                FistName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();

            var adminIds = (await this._userManager.GetUsersInRoleAsync("Administrator")).Select(x => x.Id).ToList();

            foreach (var item in users)
            {
                item.IsAdmin = adminIds.Contains(item.Id);
            }
            var orderedUsers = users.OrderByDescending(u => u.IsAdmin).ThenBy(u => u.UserName);

            return View(orderedUsers);
        }
        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || await _userManager.IsInRoleAsync(user,"Administrator"))
            {
                return RedirectToAction("Index");
            }
            await _userManager.AddToRoleAsync(user, "Administrator");

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || !await _userManager.IsInRoleAsync(user, "Administrator"))
            {
                return RedirectToAction("Index");
            }
            await _userManager.RemoveFromRoleAsync(user, "Administrator");

            return RedirectToAction("Index");
        }
    }
}
