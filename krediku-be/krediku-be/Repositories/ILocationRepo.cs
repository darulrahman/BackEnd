using krediku_be.Models;

namespace krediku_be.Repositories
{
    public interface ILocationRepo
    {
        Task<List<Location>> GetLocations();
        Task<Location> GetLocationById(string id);
    }
}
