#region Usings

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Gozen.Data.Entity
{
    public class DocumentType : Base.Entity
    {
        [Column] public string Type { get; set; }

        public virtual ICollection<Passenger> Passenger { get; set; }
    }
}