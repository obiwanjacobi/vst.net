namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Windows.Forms;

    public partial class EditorControl1 : UserControl
    {
        public EditorControl1()
        {
            InitializeComponent();
        }

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
