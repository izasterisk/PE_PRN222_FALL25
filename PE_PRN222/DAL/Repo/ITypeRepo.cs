using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface ITypeRepo
    {
        Task<List<LionType>> GetAllAsync();
    }
}
