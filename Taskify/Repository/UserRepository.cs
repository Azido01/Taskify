using Taskify.Models;

namespace Taskify.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public UserRepository()
        {
            // Seed data
            _users.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com", Role = "Admin" });
            _users.Add(new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Role = "User" });
        }

        public Task<List<User>> GetAllAsync() => Task.FromResult(_users);

        public Task<User?> GetByIdAsync(int id) => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public Task AddAsync(User user)
        {
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return Task.CompletedTask;
        }
    }
}
