using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;

namespace TodoAPI.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpPost("/api/v2/todo/create")]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Blog post successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the Todo item", error = ex.Message });
            }
        }

        [HttpGet("/api/v2/todo/get")]
        public async Task<IActionResult> GetAllAsync()
        {
            try{
                var todo = await _todoServices.GetAllAsync();
                if(todo == null || !todo.Any()) 
                    return Ok(new { message = "No Todo items found" });
                return Ok(new { message = "Successfully retrieved all blog posts", data = todo });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred whule retrieving all Todo it posts", error = ex.Message });
            }
        }

        [HttpGet("/api/v2/todo/getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Now Todo item with Id {id} not found."});
                }
                return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = todo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}.", error = ex.Message });
            }
        }

        [HttpPut("/api/v2/todo/update/{id}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo Item with Id {id} not found."});
                }
                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $"Todo Item with Id {id} successfully updated."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occured while updating blow post with Id {id}", error = ex.Message });
            }
        }

        [HttpDelete("/api/v2/todo/delete/{id}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = $"Todo item with Id {id} successfully deleted."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Todo item with Id {id}", error = ex.Message });
            }
        }
    }
}