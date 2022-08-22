using Cinema.Domain.DomainModels;
using Cinema.Domain.DTO;
using Cinema.Repository.Interface;
using Cinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<MovieInMovieCard> _movieInMovieCardRepository;
        private readonly IUserRepository _userRepository;
       
        
        public MovieService(IRepository<Movie> movieRepository, IRepository<MovieInMovieCard> movieInMovieCardRepository, IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _movieInMovieCardRepository = movieInMovieCardRepository;
            _userRepository = userRepository;
        }



        public bool AddToMovieCard(AddToMovieCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userMovieCard = user.UserCard;
            if(item.MovieId !=null && userMovieCard != null)
            {
                var movie = this.GetDetailsForMovie(item.MovieId);
                if(movie != null)
                {
                    MovieInMovieCard itemToAdd = new MovieInMovieCard
                    {
                        Id = Guid.NewGuid(),
                        Movie = movie,
                        MovieId = movie.Id,
                        MovieCard = userMovieCard,
                        MovieCardId = userMovieCard.Id,
                        Quantity  = item.Quantity

                    };
                this._movieInMovieCardRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }   

        public void CreateNewMovie(Movie m)
        {
            this._movieRepository.Insert(m);
        }

        public void DeleteMovie(Guid id)
        {
            var movie = this.GetDetailsForMovie(id);
            this._movieRepository.Delete(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return this._movieRepository.GetAll().ToList();
        }

        public Movie GetDetailsForMovie(Guid? id)
        {
            return this._movieRepository.Get(id);
        }

        public AddToMovieCardDto GetMovieCardInfo(Guid? id)
        {
            var movie = this.GetDetailsForMovie(id);
            AddToMovieCardDto model = new AddToMovieCardDto
            {
                SelectedMovie = movie,
                MovieId = movie.Id,
                Quantity = 1,
            };
            return model;
        }

        public void UpdateExistingMovie(Movie m)
        {
            this._movieRepository.Update(m);
        }
    }
}
