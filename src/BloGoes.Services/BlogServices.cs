using BloGoes.Data.Repository.Interfaces;
using BloGoes.Domain.DTO;
using BloGoes.Domain.Entity;
using BloGoes.Services.Services.Interfaces;

namespace BloGoes.Services
{

    public class BlogServices : IBlogServices
    {
        private readonly IBlogRepository _blogRepository;

        public BlogServices(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<BlogEntity> Add(BlogEntity entity)
        {

            return await _blogRepository.AddAsync(entity);
        }
        public async Task<BlogEntity> Update(BlogUpdateDTO entity)
        {
            var _entity = await _blogRepository.GetByIdAsync(entity.Id) ?? throw new Exception("Entity Not Found");
            _entity.Update(entity);

            return await _blogRepository.UpdateAsync(_entity);
        }
        public async Task<int> Delete(int entityId)
        {
            var _entity = await _blogRepository.GetByIdAsync(entityId) ?? throw new Exception("Entity Not Found");
            return await _blogRepository.RemoveAsync(entityId);
        }
        public async Task<IEnumerable<BlogEntity>> List()
        {
            return await _blogRepository.GetAllAsync();
        }
        public async Task<BlogEntity> GetById(int entityId)
        {
            return await _blogRepository.GetByIdAsync(entityId);
        }
    }
}
