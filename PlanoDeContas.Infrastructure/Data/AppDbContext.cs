using Microsoft.EntityFrameworkCore;
using PlanoDeContas.Domain.Entities;

namespace PlanoDeContas.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<PlanoDeConta> PlanoDeContas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlanoDeConta>()
                .HasIndex(p => p.Codigo)
                .IsUnique();

            modelBuilder.Entity<PlanoDeConta>()
                .HasOne(p => p.Pai)
                .WithMany(p => p.Filhos)
                .HasForeignKey(p => p.PaiId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
