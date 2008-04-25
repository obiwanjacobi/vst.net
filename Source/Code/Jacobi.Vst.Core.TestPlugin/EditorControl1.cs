using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jacobi.Vst.Core.TestPlugin
{
    public partial class EditorControl1 : UserControl
    {
        public EditorControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Hello World!");
        }
    }
}
