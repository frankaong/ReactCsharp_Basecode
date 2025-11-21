using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.DTO;
using ASI.Basecode.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task AddAsync(Ticket ticket)
        {
            await _ticketRepository.AddAsync(ticket);
        }

        public Ticket GetById(int id)
        {
            return _ticketRepository.GetById(id);
        }
        
        public IEnumerable<Ticket> GetTickets()
        {
            return _ticketRepository.GetAll();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            await _ticketRepository.DeleteAsync(ticket);
        }

        public async Task AssignTicketAsync(int ticketId, int assignedTo)
        {
            await _ticketRepository.AssignTicketAsync(ticketId, assignedTo);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            var currentTicket = _ticketRepository.GetById(ticket.Id);
            if (currentTicket == null)
                throw new Exception("Ticket not found.");

            currentTicket.Title = ticket.Title;
            currentTicket.Description = ticket.Description;
            currentTicket.Category = ticket.Category;
            currentTicket.Priority = ticket.Priority;
            currentTicket.Attachment = ticket.Attachment;
            currentTicket.AssignedTo = ticket.AssignedTo;
            currentTicket.DueDate = ticket.DueDate;
            currentTicket.Status = ticket.Status;

            await _ticketRepository.UpdateAsync(currentTicket);
        }

        public async Task UnassignTicketAsync(int id)
        {
            await _ticketRepository.UnassignTicketAsync(id);
        }

        public async Task AutoMarkOverdueAsync()
        {
            await _ticketRepository.AutoMarkOverdueAsync();
        }

        public async Task<string?> UploadAttachmentAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var savedFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, savedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return savedFileName;
        }

        public async Task<Ticket> CreateTicketAsync(CreateTicketDto dto)
        {
            string? savedFileName = await UploadAttachmentAsync(dto.Attachment);

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Priority = dto.Priority,
                AssignedTo = dto.AssignedTo,
                DueDate = dto.DueDate,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy,
                Attachment = savedFileName,   
            };

            await _ticketRepository.AddAsync(ticket);

            return ticket;
        }




    }
}

