using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestDeptsApp.Models.DataServices;
using TestDeptsApp.Models.DataContexts;
using TestDeptsApp.Models.DataObjects;

namespace TestDeptsApp
{
    public partial class fmEditWorker : Form
    {
        public Worker EditingWorker { get; }
        TdDataService _dataService;
        public fmEditWorker(Worker editingWorker, TdDataService service)
        {
            InitializeComponent();
            EditingWorker = editingWorker;
            _dataService = service;
        }

        private void fmEditWorker_Load(object sender, EventArgs e)
        {
            cbDepts.DataSource = _dataService.Depts.GetAll();
            cbDepts.ValueMember = "Id";
            cbDepts.DisplayMember = "Name";
            cbPositions.DataSource = _dataService.Positions.GetAll();
            cbPositions.ValueMember = "Id";
            cbPositions.DisplayMember = "Name";
            SetBinding();
            EditingWorker.PropertyChanged += WorkerPropertyChanged;
        }

        private void WorkerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EditingWorker.IsChanged = true;
        }

        void SetBinding()
        {
            tbFullName.DataBindings.Add(new Binding("Text", EditingWorker, "FullName"));
            tbPersonnelNumber.DataBindings.Add(new Binding("Text", EditingWorker, "PersonnelNumber"));
            cbDepts.DataBindings.Add(new Binding("SelectedValue", EditingWorker, "DeptId", true, DataSourceUpdateMode.OnPropertyChanged));
            cbPositions.DataBindings.Add(new Binding("SelectedValue", EditingWorker, "PositionId", true, DataSourceUpdateMode.OnPropertyChanged));
            rbFemale.DataBindings.Add(new Binding("Checked", EditingWorker, "IsFemale"));
            rbMale.DataBindings.Add(new Binding("Checked", EditingWorker, "IsMale"));
            dtpBirthdate.DataBindings.Add(new Binding("Value", EditingWorker, "Birthdate", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            numSalary.DataBindings.Add(new Binding("Value", EditingWorker, "Salary", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
            cbFired.DataBindings.Add(new Binding("Checked", EditingWorker, "IsFired"));
            tbNote.DataBindings.Add(new Binding("Text", EditingWorker, "Note"));
        }

        string ValidateWorker()
        {
            string errorMsg = "";
            if (EditingWorker.FullName == String.Empty || EditingWorker.PersonnelNumber == String.Empty)
            {
                errorMsg += "ФИО и Табельный номер - обязательные поля." + Environment.NewLine;
            }

            return errorMsg;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string error = ValidateWorker();
            if (error != String.Empty)
            {
                MessageBox.Show(error, "Ошибка");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void fmEditWorker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EditingWorker.IsChanged)
            {
                if (MessageBox.Show("На форме есть несохраненные изменения. Вы уверены, что хотите закрыть форму?",
                                    "Подтверждение", 
                                    MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
            }
        }
    }
}
