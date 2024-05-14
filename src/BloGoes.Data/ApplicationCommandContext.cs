using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Data
{
    public class ApplicationCommandContext : BaseContext
    {
        public ApplicationCommandContext(DbContextOptions<ApplicationCommandContext> options) : base(options)
        {
            
        }
    }
}
