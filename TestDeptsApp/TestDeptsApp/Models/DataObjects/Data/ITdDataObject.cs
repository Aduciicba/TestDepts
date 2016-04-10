using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeptsApp.Models.DataObjects
{
    public abstract class ITdDataObject : INotifyPropertyChanged
    {
        int _id;
        bool _isChanged;

        public int Id
        {
            get
            {
                return _id;
            }

            internal set
            {
                if (_id == value)
                    return;
                _id = value;
            }
        }

        [NotMapped]
        public bool IsNew
        {
            get
            {
                return _id == -1;
            }
        }

        [NotMapped]
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            set
            {
                if (_isChanged == value)
                    return;
                _isChanged = value;
            }
        }



        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
