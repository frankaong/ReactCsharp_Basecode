using System;
using Microsoft.AspNetCore.Http;

namespace ASI.Basecode.Services.DTO
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public IFormFile? Attachment { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
