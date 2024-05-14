using BloGoes.Domain.Entity;

namespace BloGoes.Services.Services.Interfaces
{
    public interface IBlogUserServices
    {
        public Task<BlogUserEntity> Add(BlogUserEntity entity);
        public Task<BlogUserEntity> Update(BlogUserEntity entity);
        public Task<int> Delete(int entityId);
        public Task<IEnumerable<BlogUserEntity>> List();
        public Task<BlogUserEntity> GetById(int entityId);
        public Task<BlogUserEntity> GetByEmail(string email);

    }
}
