using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Model
{
  public class MovieContext : DbContext
  {
    //this is actual entity object linked to the movies in our DB
    public DbSet<Movie> Movies { get; set; }
    //this is actual entity object linked to the actors in our DB
    public DbSet<Actor> Actors { get; set; }

    //this method is run automatically by EF the first time we run the application
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
      //here we define the name of our database
      optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=MovieDB;Pooling=true;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(m => m.Movie)
                .WithMany(r => r.MovieActors)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(a => a.Actor)
                .WithMany(r => r.MovieActors)
                .HasForeignKey(a => a.ActorId);
        }

    }
}
  

  //this is the typed representation of a movie in our project
 public class Movie
    {

        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<MovieActor> MovieActors { get; set; }
    }


  //this is the typed representation of an actor in our project
  public class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }

        public List<MovieActor> MovieActors { get; set; }
    }
  public class MovieActor{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int ActorId { get; set; }
    public Actor Actor { get; set; }
    }
