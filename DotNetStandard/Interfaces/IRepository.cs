using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetStandard.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> Get<T>() where T : class;

        void Create<T>(T item) where T : class;
        void Create<T>(IEnumerable<T> items) where T : class;

        void Update<T>(T item) where T : class;
        void Update<T>(IEnumerable<T> items) where T : class;

        void Delete<T>(T item) where T : class;
        void Delete<T>(IEnumerable<T> items) where T : class;

        void Refresh<T>(T item) where T : class;
    }
}
