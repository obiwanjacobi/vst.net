using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Jacobi.Vst3.Plugin
{
    public sealed class CategoryCollection : Collection<string>
    {
        public CategoryCollection()
        {
        }

        public CategoryCollection(string parse)
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
