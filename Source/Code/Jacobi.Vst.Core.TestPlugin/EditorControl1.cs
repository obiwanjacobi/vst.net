using System;
using System.Windows.Forms;

namespace Jacobi.Vst.Core.TestPlugin
{
    public partial class EditorControl1 : UserControl
    {
        public EditorControl1()
        {
            InitializeComponent();
        }

        public void AddLine(string text)
        {
            listBox1.Items.Add(text);
        }
    }
}
