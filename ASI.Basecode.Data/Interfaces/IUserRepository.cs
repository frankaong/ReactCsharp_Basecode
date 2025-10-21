using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        User GetEmail(string email);
        Task AddAsync(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);                      
        void Delete(User user);

        void Update(User user);
        void SaveChanges();

    }
}
