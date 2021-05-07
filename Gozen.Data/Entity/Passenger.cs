using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gozen.Data.Entity
{
    public class Passenger : Entity.Base.Entity
    {
        [Required]
        [Column]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string Name { get; set; }
        [Required]
        [Column]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }
        [Required]
        [Column]
        public int Gender { get; set; }
        [Required]
        [Column]
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        [Required]
        [Column]
        [StringLength(4)]
        [Display(Name = "Document No")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public int DocumentNumber { get; set; }
    }
}
