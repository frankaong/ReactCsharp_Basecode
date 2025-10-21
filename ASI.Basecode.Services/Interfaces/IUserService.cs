using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult AuthenticateUser(string email, string password, ref User user);
        User GetEmail(string email);
        Task CreateAsync(User user);
        IEnumerable<User> GetUsers();
        User GetById(int id);
        Task UpdateAsync(User user);
        void Delete(User user);

    }
}
