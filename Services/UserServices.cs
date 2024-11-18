using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserDbContext? _context;
        private readonly ILogger<UserServices>? _logger;
        private readonly IMapper? _mapper;

        public UserServices(UserDbContext? context, ILogger<UserServices>? logger, IMapper? mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                DateTime DateTimeNow = DateTime.Now;
                var user = _mapper?.Map<User>(request);
                user.CreatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));
                user.UpdatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));
                _context?.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while creating the user.");
                throw new Exception("An error occurred while creating the user.");
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"No user found with the Id {id}");
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var user = await _context.Users.ToListAsync();
            if (user == null)
            {
                throw new Exception("No users found");
            }
            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception($"No user with Id {id} found.");
            }
            return user;
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    throw new Exception($"User with Id {id} not found.");
                }
                
                if (request.Password != null)
                {
                    user.Password = request.Password;
                }

                if (request.Nickname != null)
                {
                    user.Nickname = request.Nickname;
                }

                DateTime DateTimeNow = DateTime.Now;
                user.UpdatedAt = DateTime.Parse(DateTimeNow.ToString("dd.MM.yyyy" + " " + "HH:mm:ss"));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the user with Id {id}.");
                throw;
            }
        }
    }
}