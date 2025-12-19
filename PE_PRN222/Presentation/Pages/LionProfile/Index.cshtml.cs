using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.LionProfile
{
    public class IndexModel : PageModel
    {
        private readonly IProfileService _profileService;
        public List<DAL.Models.LionProfile> Profiles { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SearchWeight { get; set; }
        public string? SearchTypeName { get; set; }
        public int? RoleId { get; set; }
        private const int PageSize = 3;
        public IndexModel(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> OnGetAsync(int pageNumber = 1, string? weight = null, string? typeName = null)
        {
            RoleId = HttpContext.Session.GetInt32("RoleId");

            if (RoleId == null)
            {
                return RedirectToPage("/Login");
            }

            // Check permission: Only Admin(1), Manager(2), Staff(3) can view list
            if (RoleId != 1 && RoleId != 2 && RoleId != 3)
            {
                TempData["ErrorMessage"] = "You have no permission to access this function!";
                return RedirectToPage("/Login");
            }

            SearchWeight = weight;
            SearchTypeName = typeName;

            List<DAL.Models.LionProfile> allProfiles;

            if (!string.IsNullOrEmpty(weight) || !string.IsNullOrEmpty(typeName))
            {
                allProfiles = await _profileService.SearchAsync(weight, typeName);
            }
            else
            {
                allProfiles = await _profileService.GetAllAsync();
            }

            // Paging
            var totalItems = allProfiles.Count;
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            CurrentPage = pageNumber;

            Profiles = allProfiles
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }
    }
}
