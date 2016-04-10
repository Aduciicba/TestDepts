using System.Data.Entity;
using TestDeptsApp.Models.DataObjects;

namespace TestDeptsApp.Models.DataContexts
{
    class MSSqlDataContext : DbContext, ITdDataContext
    {
        DbSet<Worker> Workers { get; set; }
        DbSet<Dept> Depts { get; set; }
        DbSet<Position> Positions { get; set; }

        public MSSqlDataContext() : base(connectionString)
        {
            
        }

        public IDbSet<T> GetAll<T>() where T : ITdDataObject
        {
            return base.Set<T>();
        }

        public static string connectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["MSSQLConnectionString"]?.ConnectionString;
            }
        }


    }
}
