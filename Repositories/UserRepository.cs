using Hackathon.Data;
using Hackathon.Models;
using Hackathon.Interfaces;
using Hackathon.Dto;

namespace Hackathon.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IHasher _passwordHasher;


        public UserRepository(DataContext context, IHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public ICollection<User> GetUsers()
        {
            // This method returns all the users stored in the database
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public User? GetUser(string email)
        {
            // Returns <User> model instance or null
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public bool CheckExistance(string email)
        {
            // This method checks user presence in the database
            return GetUser(email) != null;
        }

        public bool AuthenticateUser(string email, string password)
        {
            User? uInstance = GetUser(email);

            if (uInstance != null)
                if (!uInstance.IsActive)
                    return false;
                else
                    return _passwordHasher.Verify(uInstance.PasswordHash, password);
            else
                return false;
        }

        public bool CreateUser(UserCreateDto user)
        {
            // This method is used for user creation
            // Requires email, fullname and password hash as params.
            User newUser = new User()
            {
                Id = 0,
                Email = user.Email,
                CreationDate = DateTime.Now,
                PasswordHash = _passwordHasher.GetHash(user.Password),
                IsActive = true
            };

            _context.Users.Add(newUser);

            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(string email, UserUpdateDto user)
        {
            User? oldInstance = GetUser(email);

            if (oldInstance != null)
            {
                oldInstance.Email = user.Email;
                oldInstance.IsActive = user.IsActive;

                if (user.ChangePassword)
                    oldInstance.PasswordHash = _passwordHasher.GetHash(user.Password);

                _context.Update(oldInstance);
            }

            return Save();
        }

        public bool DeleteUser(string email)
        {
            User? uInstance = GetUser(email);

            if (uInstance != null)
            {
                _context.Remove(uInstance);
            }

            return Save();
        }
    }
}
