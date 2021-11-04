using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using E_Tickets.Models.ModelsDB;

namespace E_Tickets.Models.ViewModels
{
    public class MovieViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, MinimumLength = 25, ErrorMessage = "Full Name must be between 25 and 300 chars")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(.1, 100)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public IFormFile ImageURL { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Cinema")]
        public int CinemaId { get; set; }

        [Required]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }

        [Display(Name = "Categories")]
        [Required(ErrorMessage = "Movie Category is required")]
        public List<int> Categories { get; set; }

        [Required]
        [Display(Name = "Actors")]
        public List<int> ActorId { get; set; }
    }
}
