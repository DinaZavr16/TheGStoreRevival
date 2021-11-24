using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheGStore.DAL.Models;

namespace TheGStore.DAL
{
    public class TheGStoreDbContext : IdentityDbContext
    {
        public IConfiguration Configuration { get; }

        public TheGStoreDbContext(DbContextOptions<TheGStoreDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName");
            });

            modelBuilder.Entity<Developer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Developers)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerId");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GameId)
                    .HasColumnName("gameId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.GameId);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.DeveloperId)
                    .HasColumnName("developerId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.DeveloperId);
            });
        }
    }
}
