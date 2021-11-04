using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Movie
    {
        public Movie()
        {
            ActorsMovies = new HashSet<ActorsMovie>();
            MoviesCategories = new HashSet<MoviesCategory>();
            OrderItems = new HashSet<OrderItem>();
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CinemaId { get; set; }
        public int ProducerId { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual ICollection<ActorsMovie> ActorsMovies { get; set; }
        public virtual ICollection<MoviesCategory> MoviesCategories { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
