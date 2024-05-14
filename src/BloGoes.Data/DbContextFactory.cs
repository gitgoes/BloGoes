using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Data
{
    public class DbContextFactory
    {
        private readonly IDictionary<string, BaseContext> _context;

        public DbContextFactory(IDictionary<string, BaseContext> context)
        {
            _context = context;
        }

        public BaseContext GetContext(string contextName)
        {
            return _context[contextName];
        }
    }
}
