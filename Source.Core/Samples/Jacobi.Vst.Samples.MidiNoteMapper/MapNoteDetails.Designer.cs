namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    partial class MapNoteDetails
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
            this.KeyNameTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OutputNoteNoTxt = new System.Windows.Forms.NumericUpDown();
            this.TriggerNoteNoTxt = new System.Windows.Forms.NumericUpDown();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OutputNoteNoTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TriggerNoteNoTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(56, 13);
            label1.TabIndex = 0;
            label1.Text = "Key Name";
            // 
            // KeyNameTxt
            // 
            this.KeyNameTxt.Location = new System.Drawing.Point(75, 10);
            this.KeyNameTxt.Name = "KeyNameTxt";
            this.KeyNameTxt.Size = new System.Drawing.Size(101, 20);
            this.KeyNameTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trigger Note #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output Note #";
            // 
            // OutputNoteNoTxt
            // 
            this.OutputNoteNoTxt.Location = new System.Drawing.Point(94, 72);
            this.OutputNoteNoTxt.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.OutputNoteNoTxt.Name = "OutputNoteNoTxt";
            this.OutputNoteNoTxt.Size = new System.Drawing.Size(53, 20);
            this.OutputNoteNoTxt.TabIndex = 4;
            // 
            // TriggerNoteNoTxt
            // 
            this.TriggerNoteNoTxt.Location = new System.Drawing.Point(94, 40);
            this.TriggerNoteNoTxt.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.TriggerNoteNoTxt.Name = "TriggerNoteNoTxt";
            this.TriggerNoteNoTxt.Size = new System.Drawing.Size(53, 20);
            this.TriggerNoteNoTxt.TabIndex = 4;
            // 
            // OKBtn
            // 
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKBtn.Location = new System.Drawing.Point(205, 10);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 5;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.CausesValidation = false;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Location = new System.Drawing.Point(205, 40);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // MapNoteDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(292, 104);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.TriggerNoteNoTxt);
            this.Controls.Add(this.OutputNoteNoTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KeyNameTxt);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapNoteDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Note Mapping";
            this.Load += new System.EventHandler(this.MapNoteDetails_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapNoteDetails_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.OutputNoteNoTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TriggerNoteNoTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KeyNameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown OutputNoteNoTxt;
        private System.Windows.Forms.NumericUpDown TriggerNoteNoTxt;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}