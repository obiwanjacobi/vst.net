namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Maintains information on the file selector provided by the host.
    /// </summary>
    public class VstFileSelect
    {
        /// <summary>Contains an unmanaged pointer.</summary>
        public IntPtr Reserved;

        /// <summary>
        /// The type of file selector.
        /// </summary>
        /// <remarks>Fill this field before call the OpenFileSelector method.</remarks>
        public VstFileSelectCommand Command { get; set; }

        /// <summary>
        /// The file types to filter on.
        /// </summary>
        /// <remarks>Fill this field before call the OpenFileSelector method.</remarks>
        public VstFileType[] FileTypes { get; set; }

        private String _title;
        /// <summary>
        /// The title displayed on the dialog.
        /// </summary>
        /// <remarks>Fill this field before call the OpenFileSelector method.
        /// The number of characters must not exceed 1024.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 1024 characters.</exception>
        public string Title
        {
            get { return _title; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxFileSelectorTitle, "Title");
                
                _title = value;
            }
        }

        /// <summary>
        /// The directory initialy selected in the selector.
        /// </summary>
        /// <remarks>Fill this field before call the OpenFileSelector method.</remarks>
        public string InitialPath { get; set; }

        /// <summary>
        /// The paths to the files the user selected.
        /// </summary>
        /// <remarks>This field is filled with zero, one or more file paths when the OpenFileSelector method returns.</remarks>
        public string[] ReturnPaths { get; set; }
    }

    /// <summary>
    /// Indicates to the host what the file selector is supposed to do.
    /// </summary>
    public enum VstFileSelectCommand
    {
        /// <summary>For loading a file.</summary>
        FileLoad = 0,
        /// <summary>For saving a file.</summary>
        FileSave,
        /// <summary>For loading multiple files.</summary>
        MultipleFilesLoad,
        /// <summary>For selecting a directory/folder.</summary>
        DirectorySelect
    }

    /// <summary>
    /// Information about the file filter types for the file selector.
    /// </summary>
    public class VstFileType
    {
        private string _name;
        /// <summary>
        /// Gets or sets the name of the filter.
        /// </summary>
        /// <remarks>The number of characters must not exceed 128.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 128 characters.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxFileTypeName, "Name");

                _name = value;
            }
        }
        private string _extension;
        /// <summary>
        /// Gets or sets the file extension for the file filter.
        /// </summary>
        /// <remarks>The number of characters must not exceed 8.</remarks>
        /// <exception cref="ArgumentException">Thrown when value exceeds 8 characters.</exception>
        public string Extension
        {
            get { return _extension; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxFileTypeExtension, "Extension");

                _extension = value;
            }
        }
    }
}
