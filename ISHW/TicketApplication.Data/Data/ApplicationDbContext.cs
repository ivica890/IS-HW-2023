using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketApplication.Models;
using TicketApplication.Models.Models;
using TicketApplication.Models.Relationship;

namespace TicketApplication.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment webHostEnvironment) : base(options)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get;set; }

        public DbSet<MovieShowing> movieShowings { get; set; }

        public DbSet<CinemaHall> cinemaHalls { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        public DbSet<ShowingInShoppingCart> showingsInShoppingCart { get; set; }

        public DbSet<ShowingInOrder> showingsInOrder { get; set; }

        public DbSet<Order> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Comedy", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Drama", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Animated", DisplayOrder = 5 }
            );

            modelBuilder.Entity<Movie>().HasData(
            new Movie {Id=1, Name= "Law abaiding citizen", Description = "A frustrated man decides to take justice into his own hands after a plea bargain sets one of his family's killers free.", Duration = 149, ReleaseYear=2009, TicketPrice=200,CategoryId=1,ImageUrl= "/images/lac.jpg"},
            new Movie { Id = 4, Name = "John Wick: Chapter 4", Description = "With the price on his head ever increasing, legendary hit man John Wick takes his fight against the High Table global as he seeks out the most powerful players in the underworld, from New York to Paris to Japan to Berlin.", Duration = 120, ReleaseYear = 2023, TicketPrice = 300, CategoryId = 1, ImageUrl = "/images/j_w.jpg" },
            new Movie { Id = 3, Name = "Jumanji: The Next Level", Description = "In Jumanji: The Next Level, the gang is back but the game has changed. As they return to rescue one of their own, the players will have to brave parts unknown from arid deserts to snowy mountains, to escape the world's most dangerous game.", Duration = 123, ReleaseYear = 2019, TicketPrice = 10, CategoryId = 3, ImageUrl = "/images/.jpg" },
            new Movie { Id = 6, Name = "Baywatch", Description = "Devoted lifeguard Mitch Buchannon butts heads with a brash new recruit, as they uncover a criminal plot that threatens the future of the bay.", Duration = 116, ReleaseYear = 2017, TicketPrice = 125, CategoryId = 4, ImageUrl = "/images/bw.jpg" },
            new Movie { Id = 5, Name = "Fast & Furious", Description = "Brian O'Conner, back working for the FBI in Los Angeles, teams up with Dominic Toretto to bring down a heroin importer by infiltrating his operation.", Duration = 107, ReleaseYear = 2009, TicketPrice = 120, CategoryId = 1, ImageUrl = "/images/ff.jpg" }
                );
        }
    }
}
