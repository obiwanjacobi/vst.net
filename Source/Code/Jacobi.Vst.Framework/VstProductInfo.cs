namespace Jacobi.Vst.Framework
{
    using System;

    public class VstProductInfo
    {
        public VstProductInfo(string product, string vendor, int version)
        {
            Throw.IfArgumentTooLong(product, Core.Constants.MaxProductStringLength, "product");
            Throw.IfArgumentTooLong(vendor, Core.Constants.MaxVendorStringLength, "vendor");

            _product = product;
            _vendor = vendor;
            _version = version;
        }

        protected virtual string FormatVersion(int version)
        {
            int major = version/1000;
            version -= major;
            int minor = version/100;
            version -= minor;
            int build = version/10;
            version -= build;

            return String.Format("{0}.{1}.{2}.{3}", major, minor, build, version);
        }

        private string _product;
        public string Product
        { get { return _product; } }

        private string _vendor;
        public string Vendor
        { get { return _vendor; } }

        private string _formattedVersion;
        public string FormattedVersion
        {
            get
            {
                if (String.IsNullOrEmpty(_formattedVersion) && _version > 0)
                {
                    _formattedVersion = FormatVersion(_version);
                }

                return _formattedVersion;
            }
        }

        private int _version;
        public int Version
        { get { return _version; } }

        public bool IsValid
        {
            get { return (!String.IsNullOrEmpty(Product) && !String.IsNullOrEmpty(Vendor) && Version > 0); }
        }
    }
}
