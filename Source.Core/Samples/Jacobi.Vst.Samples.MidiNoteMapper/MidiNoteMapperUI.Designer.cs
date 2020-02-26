namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    partial class MidiNoteMapperUI
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
            this.MapListVw = new System.Windows.Forms.ListView();
            this.TriggerNoteNo = new System.Windows.Forms.ColumnHeader();
            this.KeyName = new System.Windows.Forms.ColumnHeader();
            this.SendingNoteNo = new System.Windows.Forms.ColumnHeader();
            this.AddBtn = new System.Windows.Forms.Button();
            this.EditBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MapListVw
            // 
            this.MapListVw.BackColor = System.Drawing.SystemColors.Window;
            this.MapListVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TriggerNoteNo,
            this.KeyName,
            this.SendingNoteNo});
            this.MapListVw.FullRowSelect = true;
            this.MapListVw.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.MapListVw.HideSelection = false;
            this.MapListVw.Location = new System.Drawing.Point(4, 4);
            this.MapListVw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MapListVw.MultiSelect = false;
            this.MapListVw.Name = "MapListVw";
            this.MapListVw.Size = new System.Drawing.Size(420, 216);
            this.MapListVw.TabIndex = 0;
            this.MapListVw.UseCompatibleStateImageBehavior = false;
            this.MapListVw.View = System.Windows.Forms.View.Details;
            this.MapListVw.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MapListVw_MouseDoubleClick);
            // 
            // TriggerNoteNo
            // 
            this.TriggerNoteNo.Text = "=>Note#";
            // 
            // KeyName
            // 
            this.KeyName.Text = "Key Name";
            this.KeyName.Width = 170;
            // 
            // SendingNoteNo
            // 
            this.SendingNoteNo.Text = "Note#=>";
            // 
            // AddBtn
            // 
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBtn.Location = new System.Drawing.Point(4, 226);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(100, 28);
            this.AddBtn.TabIndex = 1;
            this.AddBtn.Text = "Add...";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditBtn.Location = new System.Drawing.Point(115, 226);
            this.EditBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(100, 28);
            this.EditBtn.TabIndex = 2;
            this.EditBtn.Text = "Edit...";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.Location = new System.Drawing.Point(224, 225);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(100, 28);
            this.DeleteBtn.TabIndex = 3;
            this.DeleteBtn.Text = "Delete...";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // MidiNoteMapperUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.MapListVw);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MidiNoteMapperUI";
            this.Size = new System.Drawing.Size(429, 260);
            this.Load += new System.EventHandler(this.MidiNoteMapperUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView MapListVw;
        private System.Windows.Forms.ColumnHeader TriggerNoteNo;
        private System.Windows.Forms.ColumnHeader KeyName;
        private System.Windows.Forms.ColumnHeader SendingNoteNo;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button EditBtn;
        private System.Windows.Forms.Button DeleteBtn;

    }
}
