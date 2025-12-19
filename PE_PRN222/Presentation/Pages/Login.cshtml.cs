using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        public string? ErrorMessage { get; set; }

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            var account = await _accountService.LoginAsync(email, password);

            if (account == null)
            {
                ErrorMessage = "Invalid Email or Password!";
                return Page();
            }

            // Save to session
            HttpContext.Session.SetInt32("UserId", account.AccountId);
            HttpContext.Session.SetString("UserName", account.UserName);
            HttpContext.Session.SetString("FullName", account.FullName);
            HttpContext.Session.SetInt32("RoleId", account.RoleId);

            return RedirectToPage("/LionProfile/Index");
        }
    }
}
