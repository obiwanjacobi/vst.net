using System.Drawing;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.Host
{
    /// <summary>
    /// The frame in which a custom plugin editor UI is displayed.
    /// </summary>
    public partial class EditorFrame : Form
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public EditorFrame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Plugin Command Stub.
        /// </summary>
        public Jacobi.Vst.Core.Host.IVstPluginCommandStub PluginCommandStub { get; set; }

        /// <summary>
        /// Shows the custom plugin editor UI.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            this.Text = PluginCommandStub.Commands.GetEffectName();
            PluginCommandStub.Commands.EditorOpen(this.Handle);

            return base.ShowDialog(owner);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                PluginCommandStub.Commands.EditorClose();
            }
        }

        private void EditorFrame_Load(object sender, System.EventArgs e)
        {
            // for accurate editor window sizing handle SizeWindow on the HostCommandStub
            if (PluginCommandStub.Commands.EditorGetRect(out Rectangle wndRect))
            {
                this.Size = this.SizeFromClientSize(wndRect.Size);
            }
        }
    }
}
