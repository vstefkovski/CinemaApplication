using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public class MovieInMovieCard:BaseEntity
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid MovieCardId { get; set; }
        public MovieCard MovieCard { get; set; }
        public int Quantity { get; set; }
    }
}
