using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Library.Models;
using Library.DataRepository;

namespace Library.Controllers
{
    public class JokesController : Controller
    {

        private readonly IRepository<Joke> _repository;

        public JokesController(IRepository<Joke> repository)
        {
            _repository = repository;
        }

        // GET: Jokes
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Jokes/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View("ShowSearchForm");
        }

        // POST: Jokes/ShowSearchResults
        public IActionResult ShowSearchResults(String SearchPhrase)
        {
            return View("Index", _repository.Find(j => j.JokeQuestion.Contains(SearchPhrase)));
        }

        // GET: Jokes/Details/5
        /* public IActionResult Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }
             var joke = _repository.Get(id);
             if (joke == null)
             {
                 return NotFound();
             }

             return View(joke);
         }*/
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var joke = _repository.Get(id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // GET: Jokes/Create functionality
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create functionality 

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public IActionResult Create([Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(joke);
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Edit/5 functionality
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = _repository.Get(id);
            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        // POST: Jokes/Edit/5 functionality

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* public IActionResult Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
         {
             if (id != joke.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _repository.Update(joke);
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!_repository.Exists(joke.Id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(joke);
         }*/
        public IActionResult Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
        {
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(joke);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.Exists(joke.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }

        // GET: Jokes/Delete/5 functionality
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var joke = _repository.Get(id);
            if (joke == null)
            {
                return NotFound();
            }

            return View(joke);
        }

        // POST: Jokes/Delete/5 functionality
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
