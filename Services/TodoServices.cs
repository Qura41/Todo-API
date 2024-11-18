using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly TodoDbContext? _context;
        private readonly ILogger<TodoServices>? _logger;
        private readonly IMapper? _mapper;

        public TodoServices(TodoDbContext? context, ILogger<TodoServices>? logger, IMapper? mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                DateTime DateTimeNow = DateTime.Now;
                var todo = _mapper?.Map<Todo>(request);
                todo.CreatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));
                todo.UpdatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));
                _context?.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"No item found with the Id {id}");
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception("No Todo items found");
            }
            return todo;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new Exception($"No Todo item with Id {id} found.");
            }
            return todo;
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);

                if (todo == null)
                {
                    throw new Exception($"Todo item with Id {id} not found.");
                }
                
                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }

                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }

                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }

                if (request.DueData != null)
                {
                    todo.DueDate = request.DueData.Value;
                }

                if (request.Priority != null)
                {
                    todo.Priority = request.Priority.Value;
                }

                DateTime DateTimeNow = DateTime.Now;
                todo.UpdatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the Todo item with Id {id}.");
                throw;
            }
        }
    }
}