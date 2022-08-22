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
    public class MovieCardService : IMovieCardService
    {
        private readonly IRepository<MovieCard> _movieCardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<MovieInOrder> _movieInOrderRepository;

        public MovieCardService(IRepository<MovieCard> movieCardRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<MovieInOrder> movieInOrderRepository)
        {
            _movieCardRepository = movieCardRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _movieInOrderRepository = movieInOrderRepository;
        }


        public bool deleteMovieFromMovieCard(string userId, Guid id)
        {
            if(!string.IsNullOrEmpty(userId)&& id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userMovieCard = loggedInUser.UserCard;
                var itemToDelete = userMovieCard.MovieInMovieCards.Where(z => z.MovieId.Equals(id)).FirstOrDefault();
                userMovieCard.MovieInMovieCards.Remove(itemToDelete);
                this._movieCardRepository.Update(userMovieCard);
                return true;
            }
            return false;
        }

        public MovieCardDto getMovieCardInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);
            var userMovieCard = loggedInUser.UserCard;
            var allMovies = userMovieCard.MovieInMovieCards.ToList();
            var allMoviePrice = allMovies.Select(z => new {
            MoviePrice = z.Movie.MoviePrice,
            Quantity = z.Quantity
            }).ToList();
            double totalPrice = 0;
            foreach(var item in allMoviePrice)
            {
                totalPrice += item.MoviePrice * item.Quantity;
            }
            MovieCardDto movieCardDtoItem = new MovieCardDto
            {
                MovieInMovieCards = allMovies,
                TotalPrice = totalPrice,
            };
            return movieCardDtoItem;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userMovieCard = loggedInUser.UserCard;

                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser,
                };
                this._orderRepository.Insert(orderItem);

                List<MovieInOrder> movieInOrders = new List<MovieInOrder>();

                var result = userMovieCard.MovieInMovieCards
                    .Select(z => new MovieInOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderItem.Id,
                        MovieId = z.Movie.Id,
                        SelectedMovie = z.Movie,
                        UserOrder =orderItem
                    }).ToList();

                movieInOrders.AddRange(result);
                foreach(var item in movieInOrders)
                {
                    this._movieInOrderRepository.Insert(item);
                }

                loggedInUser.UserCard.MovieInMovieCards.Clear();

                this._userRepository.Update(loggedInUser);
                return true;


            }
            return false;
        }
    }
}
