using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.WrapperPlugin.UI;

internal partial class PluginFrame : UserControl
{
    public delegate void LoadEvent(string pluginPath);

    public event LoadEvent? OnLoadPlugin;

    public IntPtr PluginWnd { get; internal set; }

    public PluginFrame()
    {
        InitializeComponent();

        PluginWnd = PluginPanel.Handle;
    }

    private void Browse_Click(object sender, EventArgs e)
    {
        if (OpenFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            PluginPath.Text = OpenFileDialog.FileName;
            RaiseOnReload();
        }
    }

    public string LoadedPluginPath
    {
        get { return PluginPath.Text; }
        set { PluginPath.Text = value; }
    }

    public void SizeForPlugin(ref Rectangle pluginRect)
    {
        SetBounds(0, 0, pluginRect.Width, pluginRect.Height, BoundsSpecified.Size);
    }

    public void DetachPluginUI()
    {
        PluginPanel.Controls.Clear();
    }

    private void RaiseOnReload()
    {
        try
        {
            OnLoadPlugin?.Invoke(PluginPath.Text);
        }
        catch (Exception e)
        {
            MessageBox.Show(this,
                e.ToString(),
                "VST.NET 2 Wrapper Plugin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
