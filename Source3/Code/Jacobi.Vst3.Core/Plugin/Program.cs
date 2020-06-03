using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Plugin
{
    public class Program : IEnumerable<KeyValuePair<string, string>>
    {
        public Program(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        private Dictionary<string, string> _attrValues = new Dictionary<string, string>();

        public IDictionary<string, string> AttributeValues
        {
            get { return _attrValues; }
        }

        public string this[string attrId]
        {
            get
            {
                if (_attrValues.ContainsKey(attrId))
                {
                    return _attrValues[attrId];
                }

                return null;
            }
            set
            {
                _attrValues[attrId] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _attrValues.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
