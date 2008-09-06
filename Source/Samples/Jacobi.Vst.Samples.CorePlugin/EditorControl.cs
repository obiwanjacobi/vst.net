namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Windows.Forms;
    using Jacobi.Vst.Core.Plugin;
    using Jacobi.Vst.Core;

    /// <summary>
    /// This control is used as Plugin Editor UI.
    /// </summary>
    internal partial class EditorControl : UserControl
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public EditorControl()
        {
            InitializeComponent();
        }

        public IVstHostCommandStub Host { get; set; }

        /// <summary>
        /// Adds the <paramref name="text"/> to the log.
        /// </summary>
        /// <param name="text">A text to log on the UI. Must not be null.</param>
        public void AddLine(string text)
        {
            if (this.Created)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((EventHandler)delegate(object sender, EventArgs e) { listBox1.Items.Add(text); });
                }
                else
                {
                    listBox1.Items.Add(text);
                }
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
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
                    MessageBox.Show(this, fileSelect.ReturnPaths[0], Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Host.CloseFileSelector(fileSelect);
            }
        }
    }
}
