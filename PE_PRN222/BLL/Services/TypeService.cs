using DAL.Models;
using DAL.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepo _repo;

        public TypeService(ITypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<LionType>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
