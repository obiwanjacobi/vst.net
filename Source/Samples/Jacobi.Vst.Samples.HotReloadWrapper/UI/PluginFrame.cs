using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.HotReloadWrapper.UI
{
    internal partial class PluginFrame : UserControl
    {
        public delegate void ReloadEvent(string pluginPath);

        public event ReloadEvent? OnReload;

        public IntPtr PluginWnd { get; internal set; }

        public PluginFrame()
        {
            InitializeComponent();

            Reload.Enabled = false;
            PluginWnd = PluginPanel.Handle;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                PluginPath.Text = OpenFileDialog.FileName;
                RaiseOnReload();
                Reload.Enabled = true;
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

        private void Reload_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PluginPath.Text))
            {
                MessageBox.Show(this,
                    "Browse [...] for a plugin to load first.",
                    "VST.NET 2 Hot-Reload",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            RaiseOnReload();
        }

        private void RaiseOnReload()
        {
            try
            {
                OnReload?.Invoke(PluginPath.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(this,
                    e.ToString(),
                    "VST.NET 2 Hot-Reload",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
