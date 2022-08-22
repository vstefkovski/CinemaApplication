using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Domain.DomainModels
{
    public class Movie:BaseEntity
    {

        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public int Rating { get; set; }
        public int MoviePrice { get; set; }
        public virtual ICollection<MovieInMovieCard> MovieInMovieCards { get; set; }
        public virtual ICollection<MovieInOrder> Orders { get; set; }
    }
}
