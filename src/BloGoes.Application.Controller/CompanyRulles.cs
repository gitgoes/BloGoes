using BloGoes.Domain.Entity;
using BloGoes.Services.Services.Interfaces;

namespace BloGoes.Application.Controller
{
    public class BlogRulles
    {
        private readonly IBlogServices _blogServices;

        public BlogRulles(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        private void ValidaCompania(BlogEntity  blogEntity) {

          var result = blogEntity.Text ?? throw new Exception("vazio ");

            if (result == null)
            {
                throw new Exception();
            }
        }

    }
}
