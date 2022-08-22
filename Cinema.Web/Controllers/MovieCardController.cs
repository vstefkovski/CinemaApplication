using Cinema.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    public class MovieCardController : Controller
    {

        private readonly IMovieCardService _movieCardService;

        public MovieCardController(IMovieCardService movieCardService)
        {
            _movieCardService = movieCardService;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._movieCardService.getMovieCardInfo(userId));
        }
        public IActionResult DeleteMovieFromMovieCard(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._movieCardService.deleteMovieFromMovieCard(userId, productId);

            if (result)
            {
                return RedirectToAction("Index", "Movie");
            }
            else
            {
                return RedirectToAction("Index", "Movie");
            }
        }
        public IActionResult OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._movieCardService.orderNow(userId);
            if (result)
            {
                return RedirectToAction("Index", "MovieCard");
            }
            else
            {
                return RedirectToAction("Index", "MovieCard");
            }
        }
    }
}
