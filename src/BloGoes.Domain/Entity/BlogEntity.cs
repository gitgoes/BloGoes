using BloGoes.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Domain.Entity
{
    public class BlogEntity : EntityBase
    {
        public string Title { get; set; }
        public string?  Text  { get; set; }
        public DateTime PostDate { get; set; }

        public virtual  BlogUserEntity BlogUser { get; set; }
        public int BlogUserId { get; set; }

        public void Update(BlogUpdateDTO request) { 
            this.Title = request.Title;
            this.Text = request.Text;
            this.PostDate = request.PostDate;
            this.BlogUserId = request.Id;
            this.UpdateDate = DateTime.Now;
        }

    }
}