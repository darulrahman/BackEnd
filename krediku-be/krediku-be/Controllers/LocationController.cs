using krediku_be.Models;
using krediku_be.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace krediku_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationRepo _locationRepo;
        public LocationController(ILocationRepo locationRepo)
        {
            _locationRepo = locationRepo;
        }

        [HttpGet("[Action]")]
        public async Task<ActionResult<ApiMessage<List<Location>>>> GetLocations()
        {
            ApiMessage<List<Location>> apiRes = new ApiMessage<List<Location>>();
            try
            {
                List<Location> locations = await _locationRepo.GetLocations();
                
                if (locations == null || locations.Count == 0)
                    throw new Exception("Location not found or empty");

                apiRes.isSuccess = true;
                apiRes.data = locations;
            }
            catch (Exception ex)
            {
                apiRes.isSuccess = false;
                apiRes.message = ex.Message;
                if(ex.InnerException != null)
                    apiRes.message += ". InnerExc: "+ex.InnerException.Message;
            }
            finally
            {
                apiRes.messageTime = DateTime.Now;
            }

            return apiRes;
        }

        [HttpGet("[Action]/{id}")]
        public async Task<ActionResult<ApiMessage<Location>>> GetLocation(string id)
        {
            ApiMessage<Location> apiRes = new ApiMessage<Location>();
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new Exception("ID tidak valid");

                Location loc = await _locationRepo.GetLocationById(id);
                
                if (loc == null) 
                    throw new Exception("Location not Found. Please Input ID Correctly");

                apiRes.isSuccess = true;
                apiRes.data = loc;
            }
            catch (Exception ex) 
            {
                apiRes.isSuccess = false;
                apiRes.message = ex.Message;
                if (ex.InnerException != null)
                    apiRes.message += ". InnerExc: " + ex.InnerException.Message;
            }
            finally
            {
                apiRes.messageTime  = DateTime.Now;
            }
            return apiRes;
        }
    }
}
