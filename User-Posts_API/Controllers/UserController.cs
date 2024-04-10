using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var user = await _service.GetAllUserAsync();

            return Ok(user);
        }
    }
}
