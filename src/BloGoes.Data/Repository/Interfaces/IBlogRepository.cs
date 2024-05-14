using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloGoes.Domain.Entity;

namespace BloGoes.Data.Repository.Interfaces
{
    public interface IBlogRepository : IEntityRepository<BlogEntity>
    {
    }
}
