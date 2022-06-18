using PostService.Model;
using PostService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class ConnectionRepository : Repository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(AppDbContext context) : base(context) { }

        public async Task<List<Connection>> FindAll()
        {
            return _context.Connections.ToList();
        }
    }
}
