using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;
using System.Diagnostics;

namespace Jacobi.Vst.Samples.Host
{
    public partial class MainForm : Form
    {
        private List<VstPluginContext> _plugins = new List<VstPluginContext>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void FillPluginList()
        {
            PluginListVw.Items.Clear();

            foreach (VstPluginContext ctx in _plugins)
            {
                ListViewItem lvItem = new ListViewItem(ctx.PluginCommandStub.GetEffectName());
                lvItem.SubItems.Add(ctx.PluginCommandStub.GetProductString());
                lvItem.SubItems.Add(ctx.PluginCommandStub.GetVendorString());
                lvItem.SubItems.Add(ctx.PluginCommandStub.GetVendorVersion().ToString());
                lvItem.SubItems.Add(ctx.Find<string>("PluginPath"));
                lvItem.Tag = ctx;

                PluginListVw.Items.Add(lvItem);
            }
        }

        private VstPluginContext OpenPlugin(string pluginPath)
        {
            HostCommandStub hostCmdStub = new HostCommandStub();
            VstPluginContext ctx = new VstPluginContext(hostCmdStub);

            ctx.Set("PluginPath", pluginPath);
            ctx.Set("HostCmdStub", hostCmdStub);

            hostCmdStub.PluginCalled += new EventHandler<PluginCalledEventArgs>(HostCmdStub_PluginCalled);

            ctx.Initialize(pluginPath);

            return ctx;
        }

        private void ReleaseAllPlugins()
        {
            foreach (VstPluginContext ctx in _plugins)
            {
                ctx.Dispose();
            }

            _plugins.Clear();
        }

        private void HostCmdStub_PluginCalled(object sender, PluginCalledEventArgs e)
        {
            HostCommandStub hostCmdStub = (HostCommandStub)sender;

            Debug.WriteLine("Plugin " + hostCmdStub.PluginContext.PluginInfo.PluginID + " called" + e.Message);
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDlg.FileName = PluginPathTxt.Text;

            if (OpenFileDlg.ShowDialog(this) == DialogResult.OK)
            {
                PluginPathTxt.Text = OpenFileDlg.FileName;
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            VstPluginContext ctx = OpenPlugin(PluginPathTxt.Text);

            _plugins.Add(ctx);

            FillPluginList();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReleaseAllPlugins();
        }
    }
}
