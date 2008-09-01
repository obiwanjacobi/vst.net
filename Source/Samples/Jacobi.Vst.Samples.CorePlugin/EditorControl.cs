namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Windows.Forms;

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

        /// <summary>
        /// Adds the <paramref name="text"/> to the log.
        /// </summary>
        /// <param name="text">A text to log on the UI. Must not be null.</param>
        public void AddLine(string text)
        {
            if(this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate(object sender, EventArgs e) { listBox1.Items.Add(text); });
            }
            else
            {
                listBox1.Items.Add(text);
            }
        }
    }
}
