using krediku_be.Data;
using krediku_be.Models;
using Microsoft.EntityFrameworkCore;

namespace krediku_be.Repositories
{
    public class LocationRepo : ILocationRepo
    {
        private KredikuContext _context;
        public LocationRepo(KredikuContext context)
        {
            _context = context;
        }
        public async Task<Location> GetLocationById(string id)
        {
            return await _context.locations.FindAsync(id);
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _context.locations.ToListAsync();
        }
    }
}
