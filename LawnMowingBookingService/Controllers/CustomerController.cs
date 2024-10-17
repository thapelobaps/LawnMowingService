using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LawnMowingBookingService.Models; // Replace with your actual namespace
using System.Threading.Tasks;

public class CustomerController : Controller
{
    private readonly LawnMowingDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CustomerController(LawnMowingDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if role exists, if not create it
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assign the user to a role
                if (model.Role == "Manager")
                {
                    await _userManager.AddToRoleAsync(user, "Manager");

                    // Add to ConflictManagers table if it's a manager
                    var manager = new ConflictManager
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = user.PhoneNumber // Optional: Populate phone if you collect it
                    };
                    _context.ConflictManagers.Add(manager);
                    _context.SaveChanges();
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Customer");

                    // Add to Customers table if it's a customer
                    var customer = new Customer
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = user.PhoneNumber // Optional: Populate phone if you collect it
                    };
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                }

                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
}

