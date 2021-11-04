using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class eTicketDbContext : IdentityDbContext<ApplicationUser>
    {
        public eTicketDbContext()
        {
        }

        public eTicketDbContext(DbContextOptions<eTicketDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<ActorsMovie> ActorsMovies { get; set; }
        public virtual DbSet<AspNetRole> AspNetRole { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaim { get; set; }
        public virtual DbSet<AspNetUser> AspNetUser { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaim { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogin { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRole { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserToken { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MoviesCategory> MoviesCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=eTicketDb;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProfilePictureUrl).HasColumnName("ProfilePictureURL");
            });

            modelBuilder.Entity<ActorsMovie>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.MovieId });

                entity.ToTable("Actors_Movies");

                entity.HasIndex(e => e.MovieId, "IX_Actors_Movies_MovieId");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.ActorsMovies)
                    .HasForeignKey(d => d.ActorId);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ActorsMovies)
                    .HasForeignKey(d => d.MovieId);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.GivenName).IsRequired();

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Categ)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasIndex(e => e.CinemaId, "IX_Movies_CinemaId");

                entity.HasIndex(e => e.ProducerId, "IX_Movies_ProducerId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.CinemaId);

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.ProducerId);
            });

            modelBuilder.Entity<MoviesCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.MovieId });

                entity.ToTable("Movies_Categories");

                entity.HasIndex(e => e.MovieId, "IX_Movies_Categories_MovieId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MoviesCategories)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MoviesCategories)
                    .HasForeignKey(d => d.MovieId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__UserId__02925FBF");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.MovieId, "IX_OrderItems_MovieId");

                entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

                entity.Property(e => e.OrderId).IsRequired();

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MovieId);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.Bio).IsRequired();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProfilePictureUrl).HasColumnName("ProfilePictureURL");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasIndex(e => e.MovieId, "IX_ShoppingCartItems_MovieId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShoppingC__Movie__047AA831");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShoppingC__UserI__038683F8");
            });

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
