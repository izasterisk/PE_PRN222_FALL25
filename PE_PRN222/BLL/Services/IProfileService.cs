using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProfileService
    {
        Task<List<LionProfile>> GetAllAsync();
        Task<LionProfile?> GetByIdAsync(int id);
        Task<int> CreateAsync(LionProfile profile);
        Task<int> UpdateAsync(LionProfile profile);
        Task<int> DeleteAsync(int id);
        Task<List<LionProfile>> SearchAsync(string? weight, string? typeName);
    }
}
