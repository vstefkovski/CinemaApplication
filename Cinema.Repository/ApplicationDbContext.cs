using Cinema.Domain.DomainModels;
using Cinema.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CinemaApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieCard> MovieCards { get; set; }
        public virtual DbSet<MovieInMovieCard> MovieInMovieCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
               .Property(z => z.Id)
               .ValueGeneratedOnAdd();
            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<MovieInMovieCard>()
                .HasKey(z => new { z.MovieId, z.MovieCardId });

            builder.Entity<MovieInMovieCard>()
               .HasOne(z =>z.Movie)
               .WithMany(z => z.MovieInMovieCards)
               .HasForeignKey(z => z.MovieCardId);

            builder.Entity<MovieInMovieCard>()
               .HasOne(z => z.MovieCard)
               .WithMany(z => z.MovieInMovieCards)
               .HasForeignKey(z => z.MovieId);

            builder.Entity<MovieCard>()
               .HasOne<CinemaApplicationUser>(z => z.Owner)
               .WithOne(z => z.UserCard)
               .HasForeignKey<MovieCard>(z => z.OwnerId);

            builder.Entity<MovieInOrder>()
                .HasKey(z => new { z.MovieId, z.OrderId });

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.SelectedMovie)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.MovieId);

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.Movies)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
