using System.Data.Entity;
using TestDeptsApp.Models.DataObjects;

namespace TestDeptsApp.Models.DataContexts
{
    public interface ITdDataContext
    {
        int SaveChanges();
        IDbSet<T> GetAll<T>() where T : ITdDataObject;


    }
}
