using EntityLayer.DTOs.Post;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/PostServices")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IValidator<PostCreateDTO> _createValidator;
        private readonly IValidator<PostUpdateDTO> _updateValidator;

        public PostController(IPostService service, IValidator<PostCreateDTO> createValidator, IValidator<PostUpdateDTO> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }



        [HttpGet("GetAllPosts")]
        [Authorize(Roles = "Member,Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _service.GetAllPostAsync();

            return Ok(result);
        }


        [HttpGet("GetPostById/{id:Guid}")]
        [Authorize(Roles = "Member,Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPost([FromRoute] Guid id)
        {
            var result = await _service.GetSinglePostAsync(id);

            if (result == null)
            {
                return NotFound("Invalid Id!");
            }

            return Ok(result);
        }


        [HttpPost("CreatePost")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDTO model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return BadRequest();
            }

            var result = await _service.CreatePostAsync(model);

            return CreatedAtAction(nameof(GetPost), new { id = result.Id }, result);
        }


        [HttpPut("UpdatePost/{id:Guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, [FromBody] PostUpdateDTO model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return BadRequest();
            }

            var result = await _service.UpdatePostAsync(id, model);
            if (result == null)
            {
                return NotFound("Invalid Id!");
            }

            return Ok("Updated successfully!");
        }


        [HttpDelete("RemovePost/{id:Guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            var result = await _service.RemovePostAsync(id);

            if (!result)
            {
                return NotFound("Invalid Id!");
            }

            return NoContent();
        }


    }
}
