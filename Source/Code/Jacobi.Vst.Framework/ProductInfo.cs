namespace Jacobi.Vst.Framework
{
    using System;

    public class ProductInfo
    {
        public ProductInfo(string product, string vendor, int version)
        {
            _product = product;
            _vendor = vendor;
            _version = version;
            _formattedVersion = FormatVersion(version);
        }

        private string FormatVersion(int version)
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
        { get { return _formattedVersion; } }

        private int _version;
        public int Version
        { get { return _version; } }

        public bool IsValid
        {
            get { return (!String.IsNullOrEmpty(Product) && !String.IsNullOrEmpty(Vendor) && Version > 0); }
        }
    }
}
