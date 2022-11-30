namespace Jacobi.Vst.Samples.Host
{
    partial class PluginForm
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            this.PluginPropertyListVw = new System.Windows.Forms.ListView();
            this.PropertyNameHdr = new System.Windows.Forms.ColumnHeader();
            this.PropertyValueHdr = new System.Windows.Forms.ColumnHeader();
            this.ProgramListCmb = new System.Windows.Forms.ComboBox();
            this.PluginParameterListVw = new System.Windows.Forms.ListView();
            this.ParameterNameHdr = new System.Windows.Forms.ColumnHeader();
            this.ParameterValueHdr = new System.Windows.Forms.ColumnHeader();
            this.ParameterLabelHdr = new System.Windows.Forms.ColumnHeader();
            this.ParameterShortLabelHdr = new System.Windows.Forms.ColumnHeader();
            this.ProgramIndexNud = new System.Windows.Forms.NumericUpDown();
            this.OKBtn = new System.Windows.Forms.Button();
            this.GenerateNoiseBtn = new System.Windows.Forms.Button();
            this.EditorBtn = new System.Windows.Forms.Button();
            this.GenerateMidiBtn = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgramIndexNud)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.PluginPropertyListVw);
            groupBox1.Location = new System.Drawing.Point(37, 41);
            groupBox1.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(14, 16, 14, 16);
            groupBox1.Size = new System.Drawing.Size(1119, 498);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Plugin Properties";
            // 
            // PluginPropertyListVw
            // 
            this.PluginPropertyListVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyNameHdr,
            this.PropertyValueHdr});
            this.PluginPropertyListVw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginPropertyListVw.FullRowSelect = true;
            this.PluginPropertyListVw.Location = new System.Drawing.Point(14, 56);
            this.PluginPropertyListVw.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.PluginPropertyListVw.MultiSelect = false;
            this.PluginPropertyListVw.Name = "PluginPropertyListVw";
            this.PluginPropertyListVw.Size = new System.Drawing.Size(1091, 426);
            this.PluginPropertyListVw.TabIndex = 0;
            this.PluginPropertyListVw.UseCompatibleStateImageBehavior = false;
            this.PluginPropertyListVw.View = System.Windows.Forms.View.Details;
            // 
            // PropertyNameHdr
            // 
            this.PropertyNameHdr.Text = "Property Name";
            this.PropertyNameHdr.Width = 180;
            // 
            // PropertyValueHdr
            // 
            this.PropertyValueHdr.Text = "Property Value";
            this.PropertyValueHdr.Width = 180;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.ProgramListCmb);
            groupBox2.Controls.Add(this.PluginParameterListVw);
            groupBox2.Controls.Add(this.ProgramIndexNud);
            groupBox2.Location = new System.Drawing.Point(37, 558);
            groupBox2.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(8, 9, 8, 9);
            groupBox2.Size = new System.Drawing.Size(1119, 561);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Programs && Parameters";
            // 
            // ProgramListCmb
            // 
            this.ProgramListCmb.FormattingEnabled = true;
            this.ProgramListCmb.Location = new System.Drawing.Point(153, 60);
            this.ProgramListCmb.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.ProgramListCmb.Name = "ProgramListCmb";
            this.ProgramListCmb.Size = new System.Drawing.Size(942, 49);
            this.ProgramListCmb.TabIndex = 3;
            // 
            // PluginParameterListVw
            // 
            this.PluginParameterListVw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginParameterListVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ParameterNameHdr,
            this.ParameterValueHdr,
            this.ParameterLabelHdr,
            this.ParameterShortLabelHdr});
            this.PluginParameterListVw.FullRowSelect = true;
            this.PluginParameterListVw.Location = new System.Drawing.Point(20, 148);
            this.PluginParameterListVw.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.PluginParameterListVw.MultiSelect = false;
            this.PluginParameterListVw.Name = "PluginParameterListVw";
            this.PluginParameterListVw.Size = new System.Drawing.Size(1075, 386);
            this.PluginParameterListVw.TabIndex = 2;
            this.PluginParameterListVw.UseCompatibleStateImageBehavior = false;
            this.PluginParameterListVw.View = System.Windows.Forms.View.Details;
            // 
            // ParameterNameHdr
            // 
            this.ParameterNameHdr.Text = "Parameter Name";
            this.ParameterNameHdr.Width = 120;
            // 
            // ParameterValueHdr
            // 
            this.ParameterValueHdr.Text = "Value";
            this.ParameterValueHdr.Width = 50;
            // 
            // ParameterLabelHdr
            // 
            this.ParameterLabelHdr.Text = "Label";
            this.ParameterLabelHdr.Width = 80;
            // 
            // ParameterShortLabelHdr
            // 
            this.ParameterShortLabelHdr.Text = "Short Lbl";
            // 
            // ProgramIndexNud
            // 
            this.ProgramIndexNud.Location = new System.Drawing.Point(20, 63);
            this.ProgramIndexNud.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.ProgramIndexNud.Name = "ProgramIndexNud";
            this.ProgramIndexNud.Size = new System.Drawing.Size(116, 47);
            this.ProgramIndexNud.TabIndex = 0;
            this.ProgramIndexNud.ValueChanged += new System.EventHandler(this.ProgramIndexNud_ValueChanged);
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(944, 1139);
            this.OKBtn.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(212, 73);
            this.OKBtn.TabIndex = 3;
            this.OKBtn.Text = "Close";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // GenerateNoiseBtn
            // 
            this.GenerateNoiseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateNoiseBtn.Location = new System.Drawing.Point(57, 1142);
            this.GenerateNoiseBtn.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.GenerateNoiseBtn.Name = "GenerateNoiseBtn";
            this.GenerateNoiseBtn.Size = new System.Drawing.Size(238, 73);
            this.GenerateNoiseBtn.TabIndex = 4;
            this.GenerateNoiseBtn.Text = "Process Noise";
            this.GenerateNoiseBtn.UseVisualStyleBackColor = true;
            this.GenerateNoiseBtn.Click += new System.EventHandler(this.GenerateNoiseBtn_Click);
            // 
            // EditorBtn
            // 
            this.EditorBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditorBtn.Location = new System.Drawing.Point(565, 1142);
            this.EditorBtn.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.EditorBtn.Name = "EditorBtn";
            this.EditorBtn.Size = new System.Drawing.Size(212, 73);
            this.EditorBtn.TabIndex = 5;
            this.EditorBtn.Text = "Editor...";
            this.EditorBtn.UseVisualStyleBackColor = true;
            this.EditorBtn.Click += new System.EventHandler(this.EditorBtn_Click);
            // 
            // GenerateMidiBtn
            // 
            this.GenerateMidiBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateMidiBtn.Location = new System.Drawing.Point(311, 1142);
            this.GenerateMidiBtn.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.GenerateMidiBtn.Name = "GenerateMidiBtn";
            this.GenerateMidiBtn.Size = new System.Drawing.Size(238, 73);
            this.GenerateMidiBtn.TabIndex = 6;
            this.GenerateMidiBtn.Text = "Process Midi";
            this.GenerateMidiBtn.UseVisualStyleBackColor = true;
            this.GenerateMidiBtn.Click += new System.EventHandler(this.GenerateMidiBtn_Click);
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 1243);
            this.Controls.Add(this.GenerateMidiBtn);
            this.Controls.Add(this.EditorBtn);
            this.Controls.Add(this.GenerateNoiseBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plugin Details";
            this.Load += new System.EventHandler(this.PluginForm_Load);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramIndexNud)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PluginPropertyListVw;
        private System.Windows.Forms.ColumnHeader PropertyNameHdr;
        private System.Windows.Forms.ColumnHeader PropertyValueHdr;
        private System.Windows.Forms.ListView PluginParameterListVw;
        private System.Windows.Forms.NumericUpDown ProgramIndexNud;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.ColumnHeader ParameterNameHdr;
        private System.Windows.Forms.ColumnHeader ParameterValueHdr;
        private System.Windows.Forms.ColumnHeader ParameterLabelHdr;
        private System.Windows.Forms.ColumnHeader ParameterShortLabelHdr;
        private System.Windows.Forms.Button GenerateNoiseBtn;
        private System.Windows.Forms.Button EditorBtn;
        private System.Windows.Forms.ComboBox ProgramListCmb;
        private System.Windows.Forms.Button GenerateMidiBtn;
    }
}