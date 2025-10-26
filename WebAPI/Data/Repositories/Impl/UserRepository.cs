using System.Collections.Generic;
using System.Linq;
using WebAPI.Data.Entities;

namespace WebAPI.Data.Repositories.Impl
{
    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private AppDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The DB context.</param>
        public UserRepository(AppDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Gets the User by email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>Found user or null.</returns>
        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Gets the User by Id.
        /// </summary>
        /// <param name="id">The user Id.</param>
        /// <returns>Found user or null.</returns>
        public User? GetById(int id)
        {
            return _db.Users.Find(id);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users.</returns>
        public IList<User> GetAll()
        {
            Console.WriteLine($"🔍 DEBUG: Getting all users from database...");
            var users = _db.Users.ToList();
            Console.WriteLine($"✅ DEBUG: Retrieved {users.Count} users from database");
            return users;
        }

        /// <summary>
        /// Creates the given user.
        /// </summary>
        /// <param name="entity">The user entity.</param>
        /// <returns>Created user entity.</returns>
        public User Create(User entity)
        {
            Console.WriteLine($"➕ DEBUG: Creating new user: {entity.Name} ({entity.Email})");
            var newEntity = _db.Users.Add(entity).Entity;
            _db.SaveChanges();
            Console.WriteLine($"✅ DEBUG: User created successfully with ID: {newEntity.Id}");
            return newEntity;
        }

        /// <summary>
        /// Updates given user.
        /// </summary>
        /// <param name="entity">The user entity.</param>
        public void Update(User entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Email = entity.Email;
                existing.PhoneNumber = entity.PhoneNumber;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes user with given Id.
        /// </summary>
        /// <param name="id">The user Id.</param>
        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                _db.Remove(existing);
                _db.SaveChanges();
            }
        }
    }
}
