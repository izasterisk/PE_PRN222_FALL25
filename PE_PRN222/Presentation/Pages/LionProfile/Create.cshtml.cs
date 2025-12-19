using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.LionProfile
{
    public class CreateModel : PageModel
    {
        private readonly IProfileService _profileService;
        private readonly ITypeService _typeService;
        public List<LionType> Types { get; set; } = new();
        public DAL.Models.LionProfile? LionProfile { get; set; }
        public Dictionary<string, string>? ValidationErrors { get; set; }
        public string? ErrorMessage { get; set; }
        public CreateModel(IProfileService profileService, ITypeService typeService)
        {
            _profileService = profileService;
            _typeService = typeService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
            {
                return RedirectToPage("/Login");
            }

            if (roleId != 1 && roleId != 2)
            {
                TempData["ErrorMessage"] = "You have no permission to access this function!";
                return RedirectToPage("/Profile/Index");
            }

            Types = await _typeService.GetAllAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string lionName, int? lionTypeId, double? weight, string characteristics, string warning)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
            {
                return RedirectToPage("/Login");
            }

            if (roleId != 1 && roleId != 2)
            {
                ErrorMessage = "You have no permission to access this function!";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(lionName))
            {
                ErrorMessage = "Name is required.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (lionName.Length <= 3)
            {
                ErrorMessage = "Name must be longer than 3 characters.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (!IsCapitalizedWords(lionName))
            {
                ErrorMessage = "Each word must start with a capital letter.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (ContainsSpecialCharacters(lionName))
            {
                ErrorMessage = "Name cannot contain special characters like #, @, &, (, ).";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (!lionTypeId.HasValue || lionTypeId.Value <= 0)
            {
                ErrorMessage = "Type is required.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (!weight.HasValue)
            {
                ErrorMessage = "Weight is required.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (weight.Value <= 30)
            {
                ErrorMessage = "Weight must be greater than 30.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(characteristics))
            {
                ErrorMessage = "Characteristics is required.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(warning))
            {
                ErrorMessage = "Warning is required.";
                Types = await _typeService.GetAllAsync();
                return Page();
            }

            var profile = new DAL.Models.LionProfile
            {
                LionName = lionName,
                LionTypeId = lionTypeId!.Value,
                Weight = weight!.Value,
                Characteristics = characteristics,
                Warning = warning
            };

            await _profileService.CreateAsync(profile);

            return RedirectToPage("Index");
        }
        private bool IsCapitalizedWords(string text)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (!char.IsUpper(word[0]))
                    return false;
            }
            return true;
        }

        private bool ContainsSpecialCharacters(string text)
        {
            return text.Contains('#') || text.Contains('@') || text.Contains('&') ||
                   text.Contains('(') || text.Contains(')');
        }
    }
}
