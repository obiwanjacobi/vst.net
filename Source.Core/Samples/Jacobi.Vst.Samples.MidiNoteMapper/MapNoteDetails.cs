using System;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    /// <summary>
    /// A form that allows the user to edit the details of a note map item.
    /// </summary>
    partial class MapNoteDetails : Form
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public MapNoteDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the note map item that is edited in the form.
        /// </summary>
        public MapNoteItem MapNoteItem { get; set; }

        private void EntityToForm()
        {
            this.KeyNameTxt.Text = MapNoteItem.KeyName;
            this.TriggerNoteNoTxt.Value = MapNoteItem.TriggerNoteNumber;
            this.OutputNoteNoTxt.Value = MapNoteItem.OutputNoteNumber;
        }

        private void FormToEntity()
        {
            MapNoteItem.KeyName = this.KeyNameTxt.Text;
            MapNoteItem.TriggerNoteNumber = (byte)this.TriggerNoteNoTxt.Value;
            MapNoteItem.OutputNoteNumber = (byte)this.OutputNoteNoTxt.Value;
        }

        private void MapNoteDetails_Load(object sender, EventArgs e)
        {
            EntityToForm();
        }

        private void MapNoteDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                FormToEntity();
            }
        }
    }
}
