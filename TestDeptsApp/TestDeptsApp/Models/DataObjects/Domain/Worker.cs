using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDeptsApp.Models.DataObjects
{
    public partial class Worker
    {
        [NotMapped]
        public bool IsFiredBool
        {
            get
            {
                return IsFired == 1;
            }
            set
            {
                if (IsFired == Convert.ToInt32(value))
                    return;
                IsFired = Convert.ToInt32(value);
            }
        }

        [Association(Name = "FK_Workers_Depts",
                     IsForeignKey = true, Storage = "_dept", 
                     ThisKey = "DeptId")]
        private EntityRef<Dept> _dept = new EntityRef<Dept>();
        public Dept Dept
        {
            get
            {
                return _dept.Entity;
            }
            set
            {
                _dept.Entity = value;
            }
        }

        [Association(Name = "FK_Workers_Positions",
                     IsForeignKey = true, Storage = "_position",
                     ThisKey = "PositionId")]
        private EntityRef<Position> _position = new EntityRef<Position>();
        public Position Position
        {
            get
            {
                return _position.Entity;
            }
            set
            {
                _position.Entity = value;
            }
        }


        [NotMapped]
        public bool IsMale
        {
            get
            {
                return SexId == SexType.Male;
            }
            set
            {
                SexId = value ? SexType.Male : SexType.Female;
            }
        }

        [NotMapped]
        public bool IsFemale
        {
            get
            {
                return SexId == SexType.Female;
            }
            set
            {
                SexId = value ? SexType.Female : SexType.Male;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}. {1}", PersonnelNumber, FullName);
        }

        public Worker Clone()
        {
            return new Worker(this.DeptId)
            {
                Id = this.Id,
                FullName = this.FullName,
                PersonnelNumber = this.PersonnelNumber,
                PositionId = this.PositionId,
                Birthdate = this.Birthdate,
                IsFired = this.IsFired,
                Note = this.Note,
                Salary = this.Salary,
                SexId = this.SexId
            };
        }

        public void CopyFromWorker(Worker w)
        {
            FullName = w.FullName;
            PersonnelNumber = w.PersonnelNumber;
            DeptId = w.DeptId;
            PositionId = w.PositionId;
            Birthdate = w.Birthdate;
            IsFired = w.IsFired;
            Note = w.Note;
            Salary = w.Salary;
            SexId = w.SexId;
        }

    }
}
