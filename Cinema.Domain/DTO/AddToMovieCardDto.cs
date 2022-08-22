using Cinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Domain.DTO
{
    public class AddToMovieCardDto
    {
        public Movie SelectedMovie { get; set; }
        public Guid MovieId { get; set; }
        public int Quantity { get; set; }
    }
}
