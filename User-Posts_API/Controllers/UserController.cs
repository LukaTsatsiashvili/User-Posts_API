﻿using EntityLayer.DTOs.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var user = await _service.GetAllUserAsync();

            return Ok(user);
        }


        [HttpGet("GetUserById/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleUser([FromRoute]Guid id)
        {           
            var user = await _service.GetUser(id);

            if (user == null)
            {
                return NotFound("User with given id does not exist!");
            }
            
            return Ok(user);
        }


        [HttpPost("CreateUser")]
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

            var result = await _service.CreateUser(model);

            return CreatedAtAction(nameof(GetSingleUser), new { id = result.Id }, result);
        }


        [HttpPut("UpdateUser/{id:Guid}")]
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

            var result = await _service.UpdateUser(id, model);
            if (result == null)
            {
                return NotFound("User with given id does not exist!");
            }

            return Ok("Updated successfuly!");
        }
    }
}