using FakeBloggingSystem.DBConfiguration;
using FakeBloggingSystem.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FakeBloggingSystem.DBContext
{
    public class BlogDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
             optionsBuilder.UseInMemoryDatabase(databaseName:"BloggingDb");
        }
        public virtual DbSet<PostDataModel> Post { get; set; }

    }
}
