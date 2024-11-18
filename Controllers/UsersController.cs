using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;

namespace TodoAPI.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("/api/v2/user/create")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                await _userServices.CreateUserAsync(request);
                return Ok(new { message = "New user successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user", error = ex.Message });
            }
        }

        [HttpGet("/api/v2/user/get")]
        public async Task<IActionResult> GetAllAsync()
        {
            try{
                var user = await _userServices.GetAllAsync();
                if(user == null || !user.Any()) 
                    return Ok(new { message = "No users found" });
                return Ok(new { message = "Successfully retrieved all users", data = user });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred whule retrieving all users", error = ex.Message });
            }
        }

        [HttpGet("/api/v2/user/getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userServices.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = $"Now users with Id {id} not found."});
                }
                return Ok(new { message = $"Successfully retrieved user with Id {id}.", data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the user with Id {id}.", error = ex.Message });
            }
        }

        [HttpPut("/api/v2/user/update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userServices.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = $"User with Id {id} not found."});
                }
                await _userServices.UpdateUserAsync(id, request);
                return Ok(new { message = $"User with Id {id} successfully updated."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occured while updating user with Id {id}", error = ex.Message });
            }
        }

        [HttpDelete("/api/v2/user/delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                await _userServices.DeleteUserAsync(id);
                return Ok(new { message = $"User with Id {id} successfully deleted."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting user with Id {id}", error = ex.Message });
            }
        }
    }
}