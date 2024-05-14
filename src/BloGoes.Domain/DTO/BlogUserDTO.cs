using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Domain.DTO
{
    public class BlogUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public IList<BlogDTO> Blogs { get; set; }
    }
}
