using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class AccountRepo : GenericRepository<LionAccount>, IAccountRepo
    {
        public AccountRepo(Su25lionDbContext context) : base(context) { }
        public async Task<LionAccount?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.LionAccounts
                .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}
