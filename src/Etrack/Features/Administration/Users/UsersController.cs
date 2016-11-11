using Etrack.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etrack.Features.Administration.Users
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public UsersController(UserManager<User> userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<UsersController>();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Retrieving all users.");
            var model = await _userManager.Users.Select(u =>
                new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    LastLoginDate = u.LastLoginDate,
                    TotalLoginsCount = u.TotalLoginsCount
                }).ToListAsync();

            return View(model);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting user {0}.", id);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {0} was not found.", id);
                return NotFound();
            }

            var model = new UserViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Email = user.Email;
            model.LastLoginDate = user.LastLoginDate;
            model.TotalLoginsCount = user.TotalLoginsCount;

            return View(model);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, FullName = model.FullName, Email = model.Email };
                _logger.LogInformation("Creating user: {0}.", model.UserName);
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User {0} created.", user.UserName);
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting user {0}.", id);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {0} was not found.", id);
                return NotFound();
            }

            var model = new EditUserViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Email = user.Email;

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Getting user {0}.", id);
                var user = await _userManager.FindByIdAsync(id);
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FullName = model.FullName;
                try
                {
                    _logger.LogInformation("Updating User: {0}.", user.UserName);
                    await _userManager.UpdateAsync(user);
                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        _logger.LogWarning("User with ID: {0} was not found", id);
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogWarning("User {0} was updated before you saved your changes, please refresh your user and try again.", user.UserName);
                        throw;
                    }
                }
                _logger.LogInformation("User {0} updated.", user.UserName);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting user {0}.", id);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {0} was not found", id);
                return NotFound();
            }

            var model = new DeleteUserViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _logger.LogInformation("Getting user {0}.", id);
            var user = await _userManager.FindByIdAsync(id);
            _logger.LogInformation("Deleting user {0}.", user.UserName);
            await _userManager.DeleteAsync(user);
            //_context.User.Remove(user);
            //await _context.SaveChangesAsync();
            _logger.LogInformation("User {0} deleted.", id);
            return RedirectToAction("Index");
        }

        private bool UserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogWarning("User creation results error: {0}", error.Description);
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
