using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Pages.LionProfile
{
    [IgnoreAntiforgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly IProfileService _profileService;
        private readonly IHubContext<SignalRHub> _hubContext;

        public DeleteModel(IProfileService profileService, IHubContext<SignalRHub> hubContext)
        {
            _profileService = profileService;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
            {
                return new JsonResult(new { success = false, message = "Not authenticated" });
            }

            if (roleId != 1 && roleId != 2)
            {
                return new JsonResult(new { success = false, message = "You have no permission to access this function!" });
            }

            var result = await _profileService.DeleteAsync(id);

            if (result > 0)
            {
                await _hubContext.Clients.All.SendAsync("ProfileDeleted", id);
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = "Failed to delete" });
        }
    }
}
