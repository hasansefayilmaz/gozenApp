using Gozen.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gozen.Data
{
    public class GozenDbContext : DbContext
    {
        public GozenDbContext(DbContextOptions<GozenDbContext> options)
            : base(options)
        {

        }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Dummy Data

            #region Document
            builder.Entity<DocumentType>().HasData(
                new DocumentType { Id = 1, Type = "Pasaport" },
                new DocumentType { Id = 2, Type = "Visa" },
                new DocumentType { Id = 3, Type = "Travel" }
            );
            builder.Entity<DocumentType>().Property(p => p.Id).UseIdentityColumn();

            #endregion

            #region Passenger

            builder.Entity<Passenger>().HasData(
                new Passenger { Id = 1, Name = "Name_1", Surname = "Surname_1", Gender = 0, DocumentTypeId = 1, DocumentNumber = 1111, },
                new Passenger { Id = 2, Name = "Name_2", Surname = "Surname_2", Gender = 1, DocumentTypeId = 2, DocumentNumber = 2222, },
                new Passenger { Id = 3, Name = "Name_3", Surname = "Surname_3", Gender = 0, DocumentTypeId = 3, DocumentNumber = 3333, },
                new Passenger { Id = 4, Name = "Name_4", Surname = "Surname_4", Gender = 1, DocumentTypeId = 1, DocumentNumber = 4444, },
                new Passenger { Id = 5, Name = "Name_5", Surname = "Surname_5", Gender = 0, DocumentTypeId = 2, DocumentNumber = 5555, },
                new Passenger { Id = 6, Name = "Name_6", Surname = "Surname_6", Gender = 1, DocumentTypeId = 3, DocumentNumber = 6666, }
                );
            #endregion

            #endregion

        }
    }
}
