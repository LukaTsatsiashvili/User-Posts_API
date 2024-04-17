using EntityLayer.DTOs.Identity.LogIn;
using EntityLayer.DTOs.Identity.Register;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.FluentValidation.Identity.LogInValidator;
using ServiceLayer.FluentValidation.Identity.RegisterValidator;
using ServiceLayer.Services.Identity.Abstract;

namespace User_Posts_API.Controllers.Identity
{
    [Route("api/AuthServices")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly RegisterRequestValidator _registerValidator;
        private readonly LogInRequestValidator _logInValidator;

        public AuthController(IAuthService service, RegisterRequestValidator registerValidator, LogInRequestValidator logInValidator)
        {
            _service = service;
            _registerValidator = registerValidator;
            _logInValidator = logInValidator;
        }


        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Registration([FromBody] RegisterRequestDTO model)
        {
            var validation = await _registerValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                return BadRequest();
            }

            var result = await _service.RegisterAsync(model);
            if (result == null)
            {
                return BadRequest("Something went wrong. Please try again later!");
            }

            return Ok("User created successfully!");
        }


        [HttpPost("LogIn")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LogIn([FromBody] LogInRequestDTO model)
        {
            var validation = await _logInValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                return BadRequest();
            }

            var result = await _service.LogInAsync(model);
            if (result == null)
            {
                return BadRequest("Something went wrong. Please try again later!");
            }

            return Ok(result);
        }
    }
}
