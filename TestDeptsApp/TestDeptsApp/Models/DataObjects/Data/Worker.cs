using System;

namespace TestDeptsApp.Models.DataObjects
{
    public partial class Worker: ITdDataObject
    {
        string _fullName;
        string _personnelNumber;
        int _deptId;
        DateTime? _birthdate;
        int? _positionId;
        int? _sexId;
        decimal? _salary;
        int _isFired;
        string _note;

        public Worker(int deptId)
        {
            Id = -1;
            DeptId = deptId;
        }
        private Worker()
        {
        }


        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (_fullName == value)
                    return;
                _fullName = value;
                NotifyPropertyChanged("FullName");
            }
        }

        public string PersonnelNumber
        {
            get
            {
                return _personnelNumber;
            }
            set
            {
                if (_personnelNumber == value)
                    return;
                _personnelNumber = value;
                NotifyPropertyChanged("PersonnelNumber");
            }
        }

        public int DeptId
        {
            get
            {
                return _deptId;
            }
            set
            {
                if (_deptId == value)
                    return;
                _deptId = value;
                NotifyPropertyChanged("DeptId");
            }
        }

        public DateTime? Birthdate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                if (_birthdate == value)
                    return;
                _birthdate = value;
                NotifyPropertyChanged("Birthdate");
            }
        }

        public int? PositionId
        {
            get
            {
                return _positionId;
            }
            set
            {
                if (_positionId == value)
                    return;
                _positionId = value;
                NotifyPropertyChanged("PositionId");
            }
        }

        public SexType? SexId
        {
            get
            {
                return (SexType)_sexId;
            }
            set
            {
                if (_sexId == (int)value)
                    return;
                _sexId = (int)value;
                NotifyPropertyChanged("SexId");
            }
        }

        public decimal? Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (_salary == value)
                    return;
                _salary = value;
                NotifyPropertyChanged("Salary");
            }
        }

        public int IsFired
        {
            get
            {
                return _isFired;
            }
            set
            {
                if (_isFired == value)
                    return;
                _isFired = value;
                NotifyPropertyChanged("IsFired");
            }
        }
        /*
        get
            {
                return _isFired == 1;
            }
            set
            {
                if (_isFired == Convert.ToInt32(value))
                    return;
                _isFired = Convert.ToInt32(value);
                NotifyPropertyChanged("IsFired");
            }
            */

        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                if (_note == value)
                    return;
                _note = value;
                NotifyPropertyChanged("Note");
            }
        }

    }
}
