using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestDeptsApp.Models.DataRepositories;

namespace TestDeptsApp.Models.DataObjects
{
    public class TreeNodeDept : TreeNode
    {
        public Dept CurrentDept { get; private set; }
        public Dept ParentDept { get; private set; }
        TdRepository<Dept> _repository;
        bool _isChildrenLoaded;

        private TreeNodeDept()
        { }
        public TreeNodeDept(TdRepository<Dept> rep, Dept dept, Dept parent = null) 
             : base(dept.Name)
        {
            CurrentDept = dept;
            _repository = rep;
            ParentDept  = parent;
            if (IsRoot)
                AddVirtualChild();
            _isChildrenLoaded = false;
        }


        public bool IsRoot
        {
            get
            {
                return CurrentDept.ParentId == null;
            }
        }

        public bool IsChildrenLoaded
        {
            get
            {
                return _isChildrenLoaded;
            }
        }

        IEnumerable<TreeNodeDept> ChildrenDepts()
        {
            var depts = _repository.GetWhere(d => d.ParentId == CurrentDept.Id);

            foreach (var dept in depts)
            {
                yield return new TreeNodeDept(_repository, dept, CurrentDept);
            }            

        }

        bool HasChildren
        {
            get
            {
                return _repository.GetCount(d => d.ParentId == CurrentDept.Id) > 0;
            }
        }

        public void AddVirtualChild()
        {
            if (HasChildren && !IsChildrenLoaded)
                base.Nodes.Add(new TreeNode());
        }

        public void AddChildrenNodes()
        {
            if (IsChildrenLoaded)
                return;
            base.Nodes.Clear();
            base.Nodes.AddRange(ChildrenDepts().ToArray());
            foreach (var node in base.Nodes)
            {
                if ((node as TreeNodeDept).HasChildren)
                    (node as TreeNodeDept).AddVirtualChild();
            }
            _isChildrenLoaded = true;
        }
    }
}
