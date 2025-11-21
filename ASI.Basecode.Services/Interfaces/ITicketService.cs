using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ITicketService
    {

        Ticket GetById(int id);
        IEnumerable<Ticket> GetTickets();
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Ticket ticket);
        Task AssignTicketAsync(int ticketId, int assignedTo);
        Task UnassignTicketAsync(int id);
        Task AutoMarkOverdueAsync();
        Task<Ticket> CreateTicketAsync(CreateTicketDto dto);
        Task<string?> UploadAttachmentAsync(IFormFile? file);


    }
}
