using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

namespace Jacobi.Vst.Samples.Host
{
    partial class MainForm : Form
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
            try
            {
                HostCommandStub hostCmdStub = new HostCommandStub();
                hostCmdStub.PluginCalled += new EventHandler<PluginCalledEventArgs>(HostCmdStub_PluginCalled);

                VstPluginContext ctx = VstPluginContext.Create(pluginPath, hostCmdStub);

                // add custom data to the context
                ctx.Set("PluginPath", pluginPath);
                ctx.Set("HostCmdStub", hostCmdStub);

                // actually open the plugin itself
                ctx.PluginCommandStub.Open();

                return ctx;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void ReleaseAllPlugins()
        {
            foreach (VstPluginContext ctx in _plugins)
            {
                // dispose of all (unmanaged) resources
                ctx.Dispose();
            }

            _plugins.Clear();
        }

        private VstPluginContext SelectedPluginContext
        {
            get
            {
                if (PluginListVw.SelectedItems.Count > 0)
                {
                    return (VstPluginContext)PluginListVw.SelectedItems[0].Tag;
                }

                return null;
            }
        }

        private void HostCmdStub_PluginCalled(object sender, PluginCalledEventArgs e)
        {
            HostCommandStub hostCmdStub = (HostCommandStub)sender;

            // can be null when called from inside the plugin main entry point.
            if (hostCmdStub.PluginContext.PluginInfo != null)
            {
                Debug.WriteLine("Plugin " + hostCmdStub.PluginContext.PluginInfo.PluginID + " called:" + e.Message);
            }
            else
            {
                Debug.WriteLine("The loading Plugin called:" + e.Message);
            }
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

            if (ctx != null)
            {
                _plugins.Add(ctx);

                FillPluginList();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReleaseAllPlugins();
        }

        private void ViewPluginBtn_Click(object sender, EventArgs e)
        {
            PluginForm dlg = new PluginForm();
            dlg.PluginContext = SelectedPluginContext;

            dlg.ShowDialog(this);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            VstPluginContext ctx = SelectedPluginContext;

            if(ctx != null)
            {
                ctx.Dispose();

                _plugins.Remove(ctx);

                FillPluginList();
            }
        }
    }
}
