namespace Jacobi.Vst.Framework
{
    public struct ProductInfo
    {
        public ProductInfo(string product, string vendor, string version)
        {
            _product = product;
            _vendor = vendor;
            _version = version;
        }

        private string _product;
        public string Product
        { get { return _product; } }

        private string _vendor;
        public string Vendor
        { get { return _vendor; } }

        private string _version;
        public string Version
        { get { return _version; } }
    }
}
