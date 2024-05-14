using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloGoes.Data.Repository.Interfaces;
using BloGoes.Domain.Entity;

namespace BloGoes.Data.Repository
{
    public class BlogRepository : EntityBaseRepository<BlogEntity>, IBlogRepository
    {
        private readonly ApplicationQuerieContext _querieContext;
        private readonly ApplicationCommandContext _commandContext;

        public BlogRepository(ApplicationQuerieContext querieContext, ApplicationCommandContext commandContext) : base(querieContext, commandContext)
        {
            _querieContext = querieContext;
            _commandContext = commandContext;
        }
    }
}
