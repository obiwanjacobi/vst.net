using System;
using System.Drawing;
using System.Windows.Forms;

using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

namespace Jacobi.Vst.Samples.Host
{
    public partial class EditorFrame : Form
    {
        public EditorFrame()
        {
            InitializeComponent();
        }

        public Jacobi.Vst.Core.Host.IVstPluginCommandStub PluginCommandStub { get; set; }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            Rectangle wndRect = new Rectangle();

            this.Text = PluginCommandStub.GetEffectName();

            if (PluginCommandStub.EditorGetRect(out wndRect))
            {
                this.Size = this.SizeFromClientSize(new Size(wndRect.Width, wndRect.Height));
                PluginCommandStub.EditorOpen(this.Handle);
            }

            return base.ShowDialog(owner);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (e.Cancel == false)
            {
                PluginCommandStub.EditorClose();
            }
        }
    }
}
