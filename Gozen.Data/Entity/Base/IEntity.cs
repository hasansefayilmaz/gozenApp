#region Usings

using System;

#endregion

namespace Gozen.Data.Entity.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime IssueDate { get; set; }
        bool IsActive { get; set; }
    }
}