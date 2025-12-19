using DAL.Models;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepo _repo;

        public ProfileService(IProfileRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<LionProfile>> GetAllAsync()
        {
            return await _repo.GetAllWithTypeAsync();
        }

        public async Task<LionProfile?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdWithTypeAsync(id);
        }

        public async Task<int> CreateAsync(LionProfile profile)
        {
            profile.ModifiedDate = DateTime.Now;
            return await _repo.CreateAsync(profile);
        }

        public async Task<int> UpdateAsync(LionProfile profile)
        {
            profile.ModifiedDate = DateTime.Now;
            return await _repo.UpdateAsync(profile);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var profile = await _repo.GetByIdAsync(id);
            if (profile != null)
            {
                return await _repo.DeleteAsync(profile);
            }
            return 0;
        }

        public async Task<List<LionProfile>> SearchAsync(string? weight, string? typeName)
        {
            return await _repo.SearchAsync(weight, typeName);
        }
    }
}
