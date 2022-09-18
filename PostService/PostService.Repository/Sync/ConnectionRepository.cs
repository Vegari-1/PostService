using Microsoft.EntityFrameworkCore;
using PostService.Model.Sync;
using PostService.Repository.Interface.Sync;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace PostService.Repository.Sync
{
    public class ConnectionRepository : Repository<Connection>, IConnectionRepository
    {
        public ConnectionRepository(AppDbContext context) : base(context) { }

        public Connection GetById(Guid id)
        {

            return _context.Connections
                            .Where(x => x.Id == id)
                            .FirstOrDefault();
        }

        public async Task<Connection> GetByIdAsync(Guid id)
        {
            return await _context.Connections
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();
        }
    }
}
