using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Dtos;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUser createuser)
        {
            if (createuser == null)
            {
                return BadRequest(new { message = "Invalid user data." });
            }

            try
            {
                var createdUser = await userService.CreateUserAsync(createuser);

                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUserAsyn(Guid id,UpdateUser updateuser)
        {
            if (updateuser == null)
            {
                return BadRequest(new { message = "Invalid user data." });
            }
            try
            {
                var updatedUser = await userService.UpdateUserAsync(id,updateuser);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            try
            {
                await userService.DeleteUserAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                
    }
}
