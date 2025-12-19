using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IProfileRepo
    {
        Task<List<LionProfile>> GetAllWithTypeAsync();
        Task<LionProfile?> GetByIdWithTypeAsync(int id);
        Task<int> CreateAsync(LionProfile profile);
        Task<int> UpdateAsync(LionProfile profile);
        Task<int> DeleteAsync(LionProfile profile);
        Task<LionProfile?> GetByIdAsync(int id);
        Task<List<LionProfile>> SearchAsync(string? weight, string? typeName);
    }
}
