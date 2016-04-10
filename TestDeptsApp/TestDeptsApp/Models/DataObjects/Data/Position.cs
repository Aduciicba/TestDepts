using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeptsApp.Models.DataObjects
{
    public partial class Position : ITdDataObject
    {
        string _name;

        private Position()
        { }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

    }
}
