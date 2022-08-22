using Cinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Services.Interface
{
    public interface IMovieCardService
    {
        MovieCardDto getMovieCardInfo(string userId);
        bool deleteMovieFromMovieCard(string userId, Guid id);
        bool orderNow(string userId);
    }
}
