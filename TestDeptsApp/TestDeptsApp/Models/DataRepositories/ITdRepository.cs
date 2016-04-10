using System;
using TestDeptsApp.Models.DataObjects;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace TestDeptsApp.Models.DataRepositories
{
    public interface ITdRepository<T> where T : ITdDataObject
    {
        ObservableCollection<T> GetAll();
        ObservableCollection<T> GetWhere(Expression<Func<T, bool>> exp);
        int GetCount(Expression<Func<T, bool>> exp);
        T GetSingle(int id);
        void Delete(T dataObject);
        void Add(T dataObject);
        int SaveChanges();
    }
}
