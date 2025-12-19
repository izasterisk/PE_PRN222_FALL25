using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class TypeRepo : GenericRepository<LionType>, ITypeRepo
    {
        public TypeRepo(Su25lionDbContext context) : base(context) { }
    }
}
