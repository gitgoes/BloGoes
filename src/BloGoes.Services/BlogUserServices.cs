using BloGoes.Data.Repository.Interfaces;
using BloGoes.Domain.Entity;
using BloGoes.Services.Services.Interfaces;

namespace BloGoes.Services
{

    public class BlogUserServices : IBlogUserServices
    {
        private readonly IBlogUserRepository _blogUserRepository;

        public BlogUserServices(IBlogUserRepository blogUserRepository)
        {
            _blogUserRepository = blogUserRepository;
        }
        public async Task<BlogUserEntity> Add(BlogUserEntity entity)
        {

            return await _blogUserRepository.AddAsync(entity);
        }
        public async Task<BlogUserEntity> Update(BlogUserEntity entity)
        {
            var _entity = await _blogUserRepository.GetByIdAsync(entity.Id) ?? throw new Exception("Entity Not Found 9+*8");
            _entity.Update(entity);

            return await _blogUserRepository.UpdateAsync(_entity);
        }
        public async Task<int> Delete(int entityId)
        {
            var _entity = await _blogUserRepository.GetByIdAsync(entityId) ?? throw new Exception("Entity Not Found");
            return await _blogUserRepository.RemoveAsync(entityId);
        }
        public async Task<IEnumerable<BlogUserEntity>> List()
        {
            return await _blogUserRepository.GetAllAsync();
        }
        public async Task<BlogUserEntity> GetById(int entityId)
        {
            return await _blogUserRepository.GetByIdAsync(entityId);
        }
        public async Task<BlogUserEntity> GetByEmail(string email)
        {
            var blogUser = await _blogUserRepository.GetAsync(predicate: f => f.Email == email);

            return blogUser == null ? throw new Exception("User not found") : blogUser?.FirstOrDefault();
        }
    }
}
