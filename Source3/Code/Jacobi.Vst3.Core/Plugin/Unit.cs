using Jacobi.Vst3.Core;

namespace Jacobi.Vst3.Plugin
{
    public class Unit
    {
        protected Unit(int id, string name, int parentId, int programListId)
        {
            Info.Id = id;
            Info.Name = name;
            Info.ParentUnitId = parentId;
            Info.ProgramListId = programListId;
        }

        public Unit(int id, string name, Unit parent, ProgramList programList)
            : this(id, name,
                    parent == null ? UnitInfo.NoParentUnitId : parent.Info.Id,
                    programList == null ? UnitInfo.NoProgramListId : programList.Id)
        {
            if (parent != null)
            {
                //Parent = parent; // collection takes care of this
                parent.Children.Add(this);
            }

            ProgramList = programList;
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
                    Children = new UnitCollection();
                }

                return _children;
            }
            protected set
            {
                _children = value;

                if (_children != null)
                {
                    _children.Parent = this;
                }
            }
        }

        public ProgramList ProgramList { get; protected set; }
    }
}
