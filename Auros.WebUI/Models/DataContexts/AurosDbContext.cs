using Auros.WebUI.Models.Entities;
using Auros.WebUI.Models.Entities.FormModels;
using Microsoft.EntityFrameworkCore;

namespace Auros.WebUI.Models.DataContexts
{
    public class AurosDbContext : DbContext
    {
        public AurosDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Subscribe> Subscribes { get; set; }

        public DbSet<SignInFormModel> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Subscribe>().HasIndex(c => c.Email).IsUnique();
        //}
    }
}