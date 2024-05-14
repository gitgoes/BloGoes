using BloGoes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Domain.DTO
{
    public class BlogDTO
    {
        public string Title { get; set; }
        public string? Text { get; set; }
        public DateTime PostDate { get; set; }
        public int BlogUserId { get; set; }
    }
}
