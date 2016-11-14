using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Etrack.Features.Administration.Roles
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public RolesController(RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<RolesController>();
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Retrieving all roles.");
            var model = await _roleManager.Roles.Select(r =>
                new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name,
                }).ToListAsync();

            return View(model);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting role {0}.", id);
            var role  = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID: {0} was not found.", id);
                return NotFound();
            }

            var model = new RoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;

            return View(model);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.RoleName };
                _logger.LogInformation("Creating role: {0}.", model.RoleName);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Role {0} created.", role.Name);
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting role {0}.", id);
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID: {0} was not found.", id);
                return NotFound();
            }

            var model = new EditRoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;

            return View(model);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditRoleViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Getting role {0}.", id);
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = model.RoleName;

                try
                {
                    _logger.LogInformation("Updating Role: {0}.", role.Name);
                    await _roleManager.UpdateAsync(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
                    {
                        _logger.LogWarning("Role with ID: {0} was not found", id);
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogWarning("Role {0} was updated before you saved your changes, please refresh your role and try again.", role.Name);
                        throw;
                    }
                }
                _logger.LogInformation("Role {0} updated.", role.Name);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            _logger.LogInformation("Getting role {0}.", id);
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID: {0} was not found", id);
                return NotFound();
            }

            var model = new DeleteRoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;

            return View(model);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _logger.LogInformation("Getting role {0}.", id);
            var role = await _roleManager.FindByIdAsync(id);
            _logger.LogInformation("Deleting role {0}.", role.Name);
            await _roleManager.DeleteAsync(role);

            _logger.LogInformation("Role {0} deleted.", id);
            return RedirectToAction("Index");
        }

        private bool RoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogWarning("Role creation results error: {0}", error.Description);
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
