namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;
    

    /// <summary>
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
        }

        public IVstHostCommandStub Host { get; set; }

        public void AddLine(string text)
        {
            if (this.IsLoaded)
            {
                this.ListBox.Items.Add(text);
            }
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            VstFileSelect fileSelect = new VstFileSelect();
            fileSelect.Command = VstFileSelectCommand.FileLoad;
            fileSelect.FileTypes = new VstFileType[2];
            fileSelect.FileTypes[0] = new VstFileType();
            fileSelect.FileTypes[0].Name = "Text Files";
            fileSelect.FileTypes[0].Extension = "txt";
            fileSelect.FileTypes[1] = new VstFileType();
            fileSelect.FileTypes[1].Name = "All Files";
            fileSelect.FileTypes[1].Extension = "*";
            fileSelect.Title = "Select a file";

            if (Host.OpenFileSelector(fileSelect))
            {
                if (fileSelect.ReturnPaths != null && fileSelect.ReturnPaths.Length > 0)
                {
                    MessageBox.Show(fileSelect.ReturnPaths[0], "VST.NET CorePlugin WPF", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Host.CloseFileSelector(fileSelect);
            }
        }
    }
}
