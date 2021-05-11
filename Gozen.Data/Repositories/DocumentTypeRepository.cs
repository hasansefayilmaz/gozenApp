#region Usings

using Gozen.Data.Core;
using Gozen.Data.Entity;

#endregion

namespace Gozen.Data.Repositories
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
    }

    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(GozenDbContext gozenDbContext) : base(gozenDbContext)
        {
        }
    }
}