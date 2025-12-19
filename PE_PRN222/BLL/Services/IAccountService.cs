using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAccountService
    {
        Task<LionAccount?> LoginAsync(string email, string password);
    }
}
