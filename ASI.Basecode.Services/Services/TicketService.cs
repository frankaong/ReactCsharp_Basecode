using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        //public async Task CreateTicket(Ticket ticket)
        //{
        //    await _ticketRepository.Add(ticket);
        //}

        //public Ticket GetById(int id)
        //{
        //    return _ticketRepository.GetById(id);
        //}

        //public IEnumerable<Ticket> GetAll()
        //{
        //    return _ticketRepository.GetAll();
        //}

        //public async Task Update(Ticket ticket)
        //{
        //    await _ticketRepository.Update(ticket);
        //}

        //public async Task Delete(int id)
        //{
        //    await _ticketRepository.Delete(id);
        //}
    }
}

