using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestDeptsApp.Models.DataServices;
using TestDeptsApp.Models.DataContexts;
using TestDeptsApp.Models.DataObjects;

namespace TestDeptsApp
{
    public partial class fmMain : Form
    {
        TdDataService _dataService;
        BindingSource _workersBinding;
        Worker _editingWorker;
        public fmMain()
        {
            InitializeComponent();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            MSSqlDataContext context = new MSSqlDataContext();
            _dataService = new TdDataService(context);            
            LoadWorkers();
            LoadDeptsTree();
        }

        private void LoadWorkers()
        {
            _workersBinding = new BindingSource();
            dgvWorkers.DataSource = _workersBinding;
        }

        void LoadDeptsTree()
        {
            Dept root = _dataService.Depts.GetWhere(d => d.ParentId == null).FirstOrDefault();
            tvDepts.Nodes.Add(new TreeNodeDept(_dataService.Depts, root));
            tvDepts.SelectedNode = tvDepts.TopNode;
        }

        Dept CurrentDept
        {
            get
            {
                return (tvDepts.SelectedNode as TreeNodeDept)?.CurrentDept;
            }
        }

        Worker CurrentWorker
        {
            get
            {
                return _workersBinding.Current as Worker;
            }
        }

        private void tvDepts_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            (e.Node as TreeNodeDept).AddChildrenNodes();
        }


        private void tvDepts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (CurrentDept == null)
                return;

            RefreshWorkers();
        }

        void RefreshWorkers(int selectedId = -1)
        {
            if (CurrentDept == null)
                return;
            Dept dept = CurrentDept;
            var workers = _dataService.Workers.GetWhere(w => w.DeptId == dept.Id);
            _workersBinding.DataSource = workers;
            if (selectedId > -1)
                _workersBinding.Position = _workersBinding.IndexOf(workers.Single(w => w.Id == selectedId));
            else
                _workersBinding.MoveFirst();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CurrentDept == null)
            {
                MessageBox.Show("Не выбрано подразделение", "Ошибка");
                return;
            }

            _editingWorker = new Worker(CurrentDept.Id)
            {
                SexId = SexType.Male,
                Birthdate = DateTime.Now,
                Salary = 1000
            };
            EditWorker(_editingWorker);
        }

        void EditWorker(Worker w)
        {
            fmEditWorker fm = new fmEditWorker(w.Clone(), _dataService);
            fm.FormClosed += EditWorkerFormClosed;
            fm.ShowDialog();
        }

        private void EditWorkerFormClosed(object sender, FormClosedEventArgs e)
        {
            fmEditWorker fm = sender as fmEditWorker;
            if (fm.DialogResult == DialogResult.Cancel)
                return;
            _editingWorker.CopyFromWorker(fm.EditingWorker);
            if (_editingWorker.IsNew)
            {
                _dataService.Workers.Add(_editingWorker);
                _workersBinding.Add(_editingWorker);
            }
            _dataService.Workers.SaveChanges();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CurrentWorker == null)
            {
                MessageBox.Show("Не выбран работник", "Ошибка");
                return;
            }

            _editingWorker = CurrentWorker;

            EditWorker(_editingWorker);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentWorker == null)
            {
                MessageBox.Show("Не выбран работник", "Ошибка");
                return;
            }
            if (MessageBox.Show(String.Format("Вы уверена, что хотите удалить работника {0}", CurrentWorker.FullName), "Подтверждение", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            _editingWorker = CurrentWorker;
            _workersBinding.Remove(CurrentWorker);
            _dataService.Workers.Delete(_editingWorker);
            _dataService.Workers.SaveChanges();
            
        }

        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть программу?",
                                "Подтверждение",
                                MessageBoxButtons.YesNo) == DialogResult.No)
            e.Cancel = true;
        }
    }

}
