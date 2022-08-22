using Cinema.Domain.DomainModels;
using Cinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Services.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();

        Movie GetDetailsForMovie(Guid? id);
        void CreateNewMovie(Movie m);
        void UpdateExistingMovie(Movie m);
        AddToMovieCardDto GetMovieCardInfo(Guid? id);
        void DeleteMovie(Guid id);
        bool AddToMovieCard(AddToMovieCardDto item, string userID);
    }
}
