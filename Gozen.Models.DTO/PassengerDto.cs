using System;
using System.ComponentModel;

namespace Gozen.Models.DTO
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Gender { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; }
        [DisplayName("Document No")]
        public int DocumentNumber { get; set; }
        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
        public virtual DocumentTypeDto DocumentType { get; set; }
    }
}