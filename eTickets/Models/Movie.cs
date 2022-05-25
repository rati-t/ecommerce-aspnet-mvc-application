using eTickets.Data.Base;
using eTickets.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Movie Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Movie name should be between 1 and 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Movie Description is required")]
        [StringLength(300, MinimumLength = 25, ErrorMessage = "Movie Description should be between 25 and 300 characters")]
        public string Description { get; set; }

        [Display(Name = "Movie Price")]
        [Required(ErrorMessage = "Movie Price is required")]
        [Range(0.01,double.MaxValue, ErrorMessage = "Movie Price should more than or uqual to 0.01")]
        public double Price { get; set; }

        [Display(Name = "Movie Profile Picture")]
        [Required(ErrorMessage = "Movie Profile Picture URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Movie Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "Movie End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Movie Category is required")]
        [EnumDataType(typeof(MovieCategory))]
        public MovieCategory MovieCategory { get; set; }

        // Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        // Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }


    }
}
