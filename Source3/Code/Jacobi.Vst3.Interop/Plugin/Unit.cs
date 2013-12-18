using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public class Unit
    {
        protected Unit(string name, int id, int parentId, int programListId)
        {
            this.Info.Id = id;
            this.Info.Name = name;
            this.Info.ParentUnitId = parentId;
            this.Info.ProgramListId = programListId;
        }

        public Unit(string name, int id, Unit parent, ProgramList programList)
            : this(name, id, 
                    parent == null ? UnitInfo.NoParentUnitId : parent.Info.Id, 
                    programList == null ? UnitInfo.NoProgramListId : programList.Id)
        {
            if (parent != null)
            {
                this.Parent = parent;
                parent.Children.Add(this);
            }
        }

        public UnitInfo Info;

        public Unit Parent { get; set; }

        private UnitCollection _children;

        public UnitCollection Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new UnitCollection();
                }

                return _children;
            }
            protected set
            {
                _children = value;
            }
        }
    }
}
