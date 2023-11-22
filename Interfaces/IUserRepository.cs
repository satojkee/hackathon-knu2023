using Hackathon.Models;
using Hackathon.Dto;

namespace Hackathon.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User? GetUser(string email);

        bool CheckExistance(string email);

        bool AuthenticateUser(string email, string passwordHash);

        bool Save();

        bool CreateUser(UserCreateDto user);

        bool UpdateUser(string email, UserUpdateDto user);

        bool DeleteUser(string email);
    }
}
