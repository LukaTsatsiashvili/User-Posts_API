using EntityLayer.DTOs.API.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.API.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/UserServices")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IValidator<UserCreateDTO> _createValidator;
        private readonly IValidator<UserUpdateDTO> _updateValidator;

        public UserController(IUserService service, IValidator<UserCreateDTO> createValidator, IValidator<UserUpdateDTO> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        [HttpGet("GetAllUsers")]
        //[Authorize(Roles = "Member,Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUser([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var result = await _service.GetAllUserAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(result);
        }


        [HttpGet("GetUserById/{id:Guid}")]
        [Authorize(Roles = "Member,Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleUser([FromRoute]Guid id)
        {           
            var result = await _service.GetSingleUserAsync(id);

            if (result == null)
            {
                return NotFound("Invalid Id!");
            }
            
            return Ok(result);
        }


        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(UserCreateDTO model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return BadRequest();
            }

            var result = await _service.CreateUserAsync(model);

            return CreatedAtAction(nameof(GetSingleUser), new { id = result.Id }, result);
        }


        [HttpPut("UpdateUser/{id:Guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDTO model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.UpdateUserAsync(id, model);
            if (result == null)
            {
                return NotFound("User with given id does not exist!");
            }

            return Ok("Updated successfuly!");
        }


        [HttpDelete("RemoveUser/{id:Guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var result = await _service.RemoveUserAsync(id);

            if (!result)
            {
                return NotFound("Invalid Id!");
            }

            return NoContent();
        }
    }
}
