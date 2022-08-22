using Cinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DTO
{
    public class MovieCardDto
    {
        public List<MovieInMovieCard> MovieInMovieCards { get; set; }
        public double TotalPrice { get; set; }
    }
}
