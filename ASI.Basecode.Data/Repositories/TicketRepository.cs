using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;

namespace ASI.Basecode.Data.Repositories
{
    public class TicketRepository: BaseRepository, ITicketRepository
    {
        public TicketRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //public async Task Add(Ticket ticket)
        //{
        //    await this.GetDbSet<Ticket>().AddAsync(ticket);
        //    UnitOfWork.SaveChanges();
        //}

        //public Ticket GetById(int id)
        //{
        //    return this.GetDbSet<Ticket>().FirstOrDefault(t => t.Id == id);
        //}

        //public IEnumerable<Ticket> GetAll()
        //{
        //    return this.GetDbSet<Ticket>().ToList();
        //}

        //public void Update(Ticket ticket)
        //{
        //    this.GetDbSet<Ticket>().Update(ticket);
        //    UnitOfWork.SaveChanges(); 
        //}

        //public void Delete(int id)
        //{
        //    var ticket = GetById(id);
        //    if (ticket != null)
        //    {
        //        this.GetDbSet<Ticket>().Remove(ticket);
        //        UnitOfWork.SaveChanges(); 
        //    }
        //}
    }
}
