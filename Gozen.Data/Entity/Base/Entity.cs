using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gozen.Data.Entity.Base
{
    [Serializable]
    public class Entity : IEntity<int>
    {
        [Key]
        [Column]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        [Column]
        public bool IsActive { get; set; } = true;
    }
}
