#region Usings

using System;

#endregion

namespace Gozen.Models.DTO
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
    }
}