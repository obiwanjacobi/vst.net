using System;

namespace ComWrapperGen
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ComInterfaceAttribute : Attribute
    {
        public ComInterfaceAttribute(string iid)
        {
            IID = Guid.Parse(iid);
        }

        public Guid IID { get; }
    }
}