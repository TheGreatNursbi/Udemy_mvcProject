using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proj.Models;
using System.Data.Entity;
using Proj.ViewModels;
using PagedList;

namespace Proj.Controllers
{
    [AllowAnonymous]
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        //Init _context
        public MovieController()    
        {
            _context = new ApplicationDbContext();
        }

        //Disposes 
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(i => i.Genre).ToList();
            return View(movies);
        }

        public ActionResult Ditails(int id)
        {
            var movie = _context.Movies.Include(i => i.Genre).SingleOrDefault(i => i.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        //Adds a new Movie
        //[Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult AddMovie()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new NewMoviewViewModel
            {
                Genres = genres
            };

            return View("FormToEditMovie", viewModel);
        }

        //Recieves disposable Movie object to add it in to DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormToCreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewMoviewViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("FormToEditMovie", viewModel);
            }
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }

        //Deletes movie
        //[Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if(movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Movie");
        }

        //Opens a form to edit
        //[Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new NewMoviewViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("FormToEditMovie", movieViewModel);
        }

        //After making some differences, updates the record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewMoviewViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("FormToEditMovie", viewModel);
            }

            if(movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var MovieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                MovieInDb.Name = movie.Name;
                MovieInDb.ReleaseDate = movie.ReleaseDate;
                MovieInDb.GenreId = movie.GenreId;
                MovieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }

        public ActionResult ApiView()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }
    }
}