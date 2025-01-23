using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureDictionary.Core.Entity;
using PictureDictionary.Core.Interfaces;

namespace PictureDictionary.API.Controller
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IFirebaseConfigService _firebaseConfig;

        public ConfigController(IFirebaseConfigService firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        [HttpGet]
        [Route("firebase")]
        public IActionResult GetFirebaseConfig()
        {
            try
            {
                return Ok(_firebaseConfig.GetAllConfig());
            }
            catch (System.Exception ex)
            {
                //logging to be added
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("test")]
        public IActionResult GetTestString()
        {
            try
            {
                return Ok("Sab MAJA MA!");
            }
            catch (System.Exception ex)
            {
                //logging to be added
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
