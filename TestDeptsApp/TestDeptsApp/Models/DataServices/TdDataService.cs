using TestDeptsApp.Models.DataObjects;
using TestDeptsApp.Models.DataRepositories;
using TestDeptsApp.Models.DataContexts;

namespace TestDeptsApp.Models.DataServices
{
    public class TdDataService
    {
        TdRepository<Worker> _workers;
        TdRepository<Dept> _depts;
        TdRepository<Position> _positions;

        ITdDataContext _mainContext;

        public TdDataService(ITdDataContext context)
        {
            _mainContext = context;
            _depts = new TdRepository<Dept>(_mainContext);
            _positions = new TdRepository<Position>(_mainContext);
            _workers = new TdRepository<Worker>(_mainContext);
        }

        public TdRepository<Worker> Workers
        {
            get
            {
                return _workers;
            }
        }

        public TdRepository<Dept> Depts
        {
            get
            {
                return _depts;
            }
        }

        public TdRepository<Position> Positions
        {
            get
            {
                return _positions;
            }
        }
    }
}
