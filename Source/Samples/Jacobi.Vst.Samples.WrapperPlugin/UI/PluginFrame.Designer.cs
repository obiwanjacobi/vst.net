namespace Jacobi.Vst.Samples.WrapperPlugin.UI
{
    partial class PluginFrame
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PluginPanel = new System.Windows.Forms.Panel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PluginPath = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PluginPanel
            // 
            this.PluginPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPanel.Location = new System.Drawing.Point(0, 43);
            this.PluginPanel.Name = "PluginPanel";
            this.PluginPanel.Size = new System.Drawing.Size(602, 341);
            this.PluginPanel.TabIndex = 0;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "dll";
            this.OpenFileDialog.Filter = "Plugins (*.dll)|*.dll";
            // 
            // PluginPath
            // 
            this.PluginPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPath.Location = new System.Drawing.Point(13, 8);
            this.PluginPath.Name = "PluginPath";
            this.PluginPath.Size = new System.Drawing.Size(536, 27);
            this.PluginPath.TabIndex = 2;
            // 
            // Browse
            // 
            this.Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Browse.Location = new System.Drawing.Point(555, 8);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(34, 29);
            this.Browse.TabIndex = 3;
            this.Browse.Text = "...";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // PluginFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.PluginPath);
            this.Controls.Add(this.PluginPanel);
            this.Name = "PluginFrame";
            this.Size = new System.Drawing.Size(602, 384);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PluginPanel;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox PluginPath;
        private System.Windows.Forms.Button Browse;
    }
}
