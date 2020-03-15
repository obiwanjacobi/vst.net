namespace Jacobi.Vst.Samples.Host
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            this.PluginListVw = new System.Windows.Forms.ListView();
            this.NameHdr = new System.Windows.Forms.ColumnHeader();
            this.ProductHdr = new System.Windows.Forms.ColumnHeader();
            this.VendorHdr = new System.Windows.Forms.ColumnHeader();
            this.VersionHdr = new System.Windows.Forms.ColumnHeader();
            this.AssemblyHdr = new System.Windows.Forms.ColumnHeader();
            this.PluginPathTxt = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.ViewPluginBtn = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(61, 13);
            label1.TabIndex = 2;
            label1.Text = "Plugin Path";
            // 
            // PluginListVw
            // 
            this.PluginListVw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginListVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHdr,
            this.ProductHdr,
            this.VendorHdr,
            this.VersionHdr,
            this.AssemblyHdr});
            this.PluginListVw.FullRowSelect = true;
            this.PluginListVw.HideSelection = false;
            this.PluginListVw.Location = new System.Drawing.Point(12, 56);
            this.PluginListVw.MultiSelect = false;
            this.PluginListVw.Name = "PluginListVw";
            this.PluginListVw.Size = new System.Drawing.Size(546, 152);
            this.PluginListVw.TabIndex = 0;
            this.PluginListVw.UseCompatibleStateImageBehavior = false;
            this.PluginListVw.View = System.Windows.Forms.View.Details;
            // 
            // NameHdr
            // 
            this.NameHdr.Text = "Name";
            this.NameHdr.Width = 120;
            // 
            // ProductHdr
            // 
            this.ProductHdr.DisplayIndex = 2;
            this.ProductHdr.Text = "Product";
            this.ProductHdr.Width = 120;
            // 
            // VendorHdr
            // 
            this.VendorHdr.DisplayIndex = 3;
            this.VendorHdr.Text = "Vendor";
            this.VendorHdr.Width = 120;
            // 
            // VersionHdr
            // 
            this.VersionHdr.DisplayIndex = 1;
            this.VersionHdr.Text = "Version";
            // 
            // AssemblyHdr
            // 
            this.AssemblyHdr.Text = "Assemlby";
            this.AssemblyHdr.Width = 120;
            // 
            // PluginPathTxt
            // 
            this.PluginPathTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPathTxt.Location = new System.Drawing.Point(12, 29);
            this.PluginPathTxt.Name = "PluginPathTxt";
            this.PluginPathTxt.Size = new System.Drawing.Size(430, 20);
            this.PluginPathTxt.TabIndex = 1;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.Location = new System.Drawing.Point(448, 27);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(31, 23);
            this.BrowseBtn.TabIndex = 3;
            this.BrowseBtn.Text = "...";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.Location = new System.Drawing.Point(485, 27);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 4;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteBtn.Location = new System.Drawing.Point(12, 214);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteBtn.TabIndex = 5;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.Filter = "Plugins (*.dll)|*.dll|All Files (*.*)|*.*";
            // 
            // ViewPluginBtn
            // 
            this.ViewPluginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewPluginBtn.Location = new System.Drawing.Point(482, 214);
            this.ViewPluginBtn.Name = "ViewPluginBtn";
            this.ViewPluginBtn.Size = new System.Drawing.Size(75, 23);
            this.ViewPluginBtn.TabIndex = 6;
            this.ViewPluginBtn.Text = "View...";
            this.ViewPluginBtn.UseVisualStyleBackColor = true;
            this.ViewPluginBtn.Click += new System.EventHandler(this.ViewPluginBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 249);
            this.Controls.Add(this.ViewPluginBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(label1);
            this.Controls.Add(this.PluginPathTxt);
            this.Controls.Add(this.PluginListVw);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VST.NET Sample Host";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView PluginListVw;
        private System.Windows.Forms.TextBox PluginPathTxt;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.ColumnHeader NameHdr;
        private System.Windows.Forms.ColumnHeader VersionHdr;
        private System.Windows.Forms.ColumnHeader ProductHdr;
        private System.Windows.Forms.ColumnHeader VendorHdr;
        private System.Windows.Forms.ColumnHeader AssemblyHdr;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.Windows.Forms.Button ViewPluginBtn;
    }
}

