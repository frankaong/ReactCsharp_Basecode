using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<User> GetUsers()
        {
            return this.GetDbSet<User>();
        }

        public User GetEmail(string email)
        {
            return this.GetDbSet<User>().FirstOrDefault(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await this.GetDbSet<User>().AddAsync(user);
            UnitOfWork.SaveChanges(); 
        }

        public IEnumerable<User> GetAll()
        {
            return this.GetDbSet<User>().ToList();
        }

        public User GetById(int id)
        {
            return this.GetDbSet<User>().FirstOrDefault(u => u.Id == id);
        }

        public void Delete(User user)
        {
            this.GetDbSet<User>().Remove(user);
            UnitOfWork.SaveChanges();
        }
        public void Update(User user)
        {
            this.GetDbSet<User>().Update(user);
        }

        public void SaveChanges()
        {
            UnitOfWork.SaveChanges();
        }

    }
}
