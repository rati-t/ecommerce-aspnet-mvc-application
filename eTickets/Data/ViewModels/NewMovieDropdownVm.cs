using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownVm
    {
        public NewMovieDropdownVm()
        {
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
            Produers = new List<Producer>();
        }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Producer> Produers { get; set; }
    }
}
