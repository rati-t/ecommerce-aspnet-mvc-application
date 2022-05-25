using eTickets.Data.Base;
using eTickets.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is Required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Start Date is Required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "End Date is Required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Movie Category is Required")]
        public MovieCategory MovieCategory { get; set; }

        // Relationships
        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Movie Actor(s) is Required")]
        public List<int> ActorsIds { get; set; }

        [Display(Name = "Select a Cinema")]
        [Required(ErrorMessage = "Cinema is Required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a Producer")]
        [Required(ErrorMessage = "Movie producer is Required")]
        public int ProducerId { get; set; }


    }
}
