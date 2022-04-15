using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo URL is required")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Cinema Name should be between 3 and 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Desctription")]
        [Required(ErrorMessage = "Desctription is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Cinema Description should be between 5 and 250 characters")]
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
