using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Jacobi.Vst3.Interop.Plugin
{
    public sealed class SubCategoryCollection : Collection<string>
    {
        public SubCategoryCollection()
        {
        }

        public SubCategoryCollection(string parse)
        {
            var cats = parse.Split(new [] {'|'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cat in cats)
            {
                Add(cat);
            }
        }

        public override string ToString()
        {
            return string.Join("|", this.ToArray());
        }
    }
}
