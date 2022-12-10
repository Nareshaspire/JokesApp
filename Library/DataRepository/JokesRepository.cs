using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataRepository
{
    public class JokesRepository : IRepository<Joke>
    {
        private readonly ApplicationDbContext _context;

        public JokesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Joke item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Joke.Any(e => e.Id == id);
        }

        public Joke? Get(int? id)
        {
            return _context.Joke.FirstOrDefault(m => m.Id == id);
        }

        public List<Joke> GetAll()
        {
            return _context.Joke.ToList();
        }

        public void Remove(int id)
        {
            var joke = _context.Joke.Find(id);
            if (joke != null)
            {
                _context.Joke.Remove(joke);
                _context.SaveChanges();
            }
        }

        public Joke Update(Joke joke)
        {
            _context.Update(joke);
            _context.SaveChanges();
            return joke;
        }

        public List<Joke> Find(Expression<Func<Joke,bool>> expression)
        {
            return _context.Joke.Where(expression).ToList();
        }
    }
}
