using System.Drawing;
using System.Windows.Forms;
using Jacobi.Vst.Core.Host;

namespace Jacobi.Vst.Samples.Host;

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
    public IVstPluginCommandStub PluginCommandStub { get; set; }

    internal DummyHostCommandStub HostCommandStub { get; set; }

    /// <summary>
    /// Shows the custom plugin editor UI.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public new DialogResult ShowDialog(IWin32Window owner)
    {
        HostCommandStub.SizeWindow += this.OnSizeWindow;

        PluginCommandStub.Commands.EditorGetRect(out var rect);
        this.Size = this.SizeFromClientSize(rect.Size);

        this.Text = PluginCommandStub.Commands.GetEffectName();
        PluginCommandStub.Commands.EditorOpen(this.Handle);

        var result = base.ShowDialog(owner);

        // to show a plugin UI without blocking the rest of the Host UI.
        //base.Show(owner);
        //result = DialogResult.OK;

        HostCommandStub.SizeWindow -= this.OnSizeWindow;
        return result;
    }

    internal void OnSizeWindow(object sender, SizeWindowEventArgs args)
    {
        this.Size = this.SizeFromClientSize(new Size(args.Width, args.Height));
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
        if (PluginCommandStub.Commands.EditorGetRect(out Rectangle wndRect))
        {
            this.Size = this.SizeFromClientSize(wndRect.Size);
        }
    }
}
