using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;


namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions<FundooContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Collaborator> Collabs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Label>()
                  .HasKey(p => new { p.UserId, p.NoteId });
            modelBuilder.Entity<Collaborator>()
                .HasKey(t => new { t.UserId, t.NoteId });
        }
    }
}
