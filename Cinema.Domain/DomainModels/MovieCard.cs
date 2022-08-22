using Cinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public class MovieCard:BaseEntity
    {
        public string OwnerId { get; set; }

        public virtual CinemaApplicationUser Owner { get; set; }

        public virtual ICollection<MovieInMovieCard> MovieInMovieCards { get; set; }

        public MovieCard()
        {

        }
    }
}
