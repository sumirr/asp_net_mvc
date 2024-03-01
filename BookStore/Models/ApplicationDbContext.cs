using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Models.ViewModels;

namespace BookStore.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } // DbSet representing  User entities
        public DbSet<Item> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly configure the User entity to map to the "User" table
            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<Item>().ToTable("Item", "dbo");
                entity.ToTable("User"); // This line maps the User entity to the "User" table

                // Additional configurations for the User entity can be placed here
                // For example, configuring primary keys, relationships, indices, etc.
                entity.HasKey(e => e.UserID); // Assuming UserID is the primary key

                // Configuring properties
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

                // Unique constraint for Email to ensure no duplicate emails
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
