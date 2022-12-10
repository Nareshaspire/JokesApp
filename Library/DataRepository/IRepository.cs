using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataRepository
{
    public interface IRepository<T>
    {
        public void Add(T item);

        public void Remove(int id);

        public T Get(int? id);

        public List<T> GetAll();

        public bool Exists(int id);

        public T Update(T item);

        public List<T> Find(Expression<Func<T, bool>> expression);
    }
}
