using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T obj);
        void Update(T obj);
        void Remove(int id);

        IEnumerable<T> GetALL();

        T Get(int? id);
    }
}
