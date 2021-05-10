using System;

namespace Gozen.Models.DTO
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Gender { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
        public virtual DocumentTypeDto DocumentType { get; set; }
    }
}