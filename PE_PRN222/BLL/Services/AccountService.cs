using DAL.Models;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _repo;
        public AccountService(IAccountRepo repo)
        {
            _repo = repo;
        }

        public async Task<LionAccount?> LoginAsync(string email, string password)
        {
            return await _repo.GetByEmailAndPasswordAsync(email, password);
        }
    }
}
