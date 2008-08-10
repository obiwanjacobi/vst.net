namespace Jacobi.Vst.Framework
{
    using System;

    /// <summary>
    /// Contains the product and vendor information.
    /// </summary>
    /// <remarks>This class is used for the host as well as for the plugin.</remarks>
    public class VstProductInfo
    {
        /// <summary>
        /// Constructs a new instance based on the <paramref name="product"/>, 
        /// <paramref name="vendor"/> and <paramref name="version"/> information.
        /// </summary>
        /// <param name="product">Must not exceed 63 characters.</param>
        /// <param name="vendor">Must not exceed 63 characters.</param>
        /// <param name="version">A version number in the thousends. Fo example 1100 means version 1.1.0.0.</param>
        public VstProductInfo(string product, string vendor, int version)
        {
            Throw.IfArgumentTooLong(product, Core.Constants.MaxProductStringLength, "product");
            Throw.IfArgumentTooLong(vendor, Core.Constants.MaxVendorStringLength, "vendor");

            Product = product;
            Vendor = vendor;
            Version = version;
        }

        /// <summary>
        /// Formats the <paramref name="version"/> to a display string.
        /// </summary>
        /// <param name="version">The version to format.</param>
        /// <returns>Never returns null.</returns>
        /// <remarks>The <paramref name="version"/> is first divided by 1000, 
        /// then by 100, then by 10 to build the display string.</remarks>
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

        /// <summary>
        /// Gets the product string.
        /// </summary>
        public string Product { get; private set; }

        /// <summary>
        /// Gets the vendor description.
        /// </summary>
        public string Vendor { get; private set; }

        private string _formattedVersion;
        /// <summary>
        /// Gets the <see cref="Version"/> formatted for displaying in the UI.
        /// </summary>
        public string FormattedVersion
        {
            get
            {
                if (String.IsNullOrEmpty(_formattedVersion) && Version > 0)
                {
                    _formattedVersion = FormatVersion(Version);
                }

                return _formattedVersion;
            }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Indicates if <see cref="Product"/>, <see cref="Vendor"/> 
        /// and <see cref="Version"/> are filled out.
        /// </summary>
        public bool IsValid
        {
            get { return (!String.IsNullOrEmpty(Product) && !String.IsNullOrEmpty(Vendor) && Version > 0); }
        }
    }
}
