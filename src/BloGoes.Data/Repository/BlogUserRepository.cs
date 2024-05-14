using BloGoes.Data.Repository.Interfaces;
using BloGoes.Domain.Entity;

namespace BloGoes.Data.Repository
{
    public class BlogUserRepository : EntityBaseRepository<BlogUserEntity>, IBlogUserRepository
    {
        private readonly ApplicationQuerieContext _querieContext;
        private readonly ApplicationCommandContext _commandContext;

        public BlogUserRepository(ApplicationQuerieContext querieContext, ApplicationCommandContext commandContext) : base(querieContext, commandContext)
        {
            _querieContext = querieContext;
            _commandContext = commandContext;
        }
    }
}
