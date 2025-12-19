using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class ProfileRepo : GenericRepository<LionProfile>, IProfileRepo
    {
        public ProfileRepo(Su25lionDbContext context) : base(context) { }
        public async Task<List<LionProfile>> GetAllWithTypeAsync()
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .OrderByDescending(p => p.LionProfileId)
                .ToListAsync();
        }

        public async Task<LionProfile?> GetByIdWithTypeAsync(int id)
        {
            return await _context.LionProfiles
                .Include(p => p.LionType)
                .FirstOrDefaultAsync(p => p.LionProfileId == id);
        }

        public async Task<List<LionProfile>> SearchAsync(string? weight, string? typeName)
        {
            var query = _context.LionProfiles.Include(p => p.LionType).AsQueryable();

            if (!string.IsNullOrEmpty(weight))
            {
                query = query.Where(p => p.Weight.ToString().Contains(weight));
            }

            if (!string.IsNullOrEmpty(typeName))
            {
                query = query.Where(p => p.LionType.LionTypeName!.Contains(typeName));
            }

            return await query.OrderByDescending(p => p.LionProfileId).ToListAsync();
        }
    }
}
