
namespace TestDeptsApp.Models.DataObjects
{
    public partial class Dept : ITdDataObject
    {
        string _name;
        int? _parentId;

        private Dept()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
            internal set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

        public int? ParentId
        {
            get
            {
                return _parentId;
            }
            internal set
            {
                if (_parentId == value)
                    return;
                _parentId = value;
            }
        }
    }
}
