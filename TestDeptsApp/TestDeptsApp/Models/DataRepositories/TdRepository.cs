using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using TestDeptsApp.Models.DataContexts;
using TestDeptsApp.Models.DataObjects;
using System.Linq.Expressions;

namespace TestDeptsApp.Models.DataRepositories
{
    public class TdRepository<T> : ITdRepository<T> where T : ITdDataObject
    {
        readonly ITdDataContext _dataContext;
        public TdRepository(ITdDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private IDbSet<T> DataSet
        {
            get
            {
                return _dataContext.GetAll<T>();
            }
        }

        public void Add(T dataObject)
        {
            DataSet.Add(dataObject);
        }

        public void Delete(T dataObject)
        {
            DataSet.Remove(dataObject);
        }

        public ObservableCollection<T> GetAll()
        {
            return new ObservableCollection<T>(DataSet.ToList());
        }

        public T GetSingle(int id)
        {
            return DataSet.Where(o => o.Id == id).SingleOrDefault();
        }

        public ObservableCollection<T> GetWhere(Expression<Func<T, bool>> exp)
        {
            return new ObservableCollection<T>(DataSet.Where(exp).ToList());
        }

        public int GetCount(Expression<Func<T, bool>> exp)
        {
            return DataSet.Count(exp);
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }


    }
}
