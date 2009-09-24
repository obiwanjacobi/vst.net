using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    /// <summary>
    /// The plugin custom editor UI.
    /// </summary>
    partial class MidiNoteMapperUI : UserControl
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public MidiNoteMapperUI()
        {
            InitializeComponent();
        }

        private MapNoteItemList _noteMap;
        /// <summary>
        /// Gets or sets the list of note map items that are shown in the editor.
        /// </summary>
        public MapNoteItemList NoteMap
        {
            get { return _noteMap; }
            set { _noteMap = value; FillList(); }
        }

        /// <summary>
        /// Contains a queue with note-on note numbers currently playing.
        /// </summary>
        public Queue<byte> NoteOnEvents { get; set; }

        /// <summary>
        /// Updates the UI with the <see cref="NoteOnEvents"/>.
        /// </summary>
        public void ProcessIdle()
        {
            if (NoteOnEvents.Count > 0)
            {
                byte noteNo;

                lock (((ICollection)NoteOnEvents).SyncRoot)
                {
                    noteNo = NoteOnEvents.Dequeue();
                }

                SelectNoteMapItem(noteNo);
            }
        }

        private void SelectNoteMapItem(byte noteNo)
        {
            MapListVw.SelectedIndices.Clear();

            if (MapListVw.Items.ContainsKey(noteNo.ToString()))
            {
                MapListVw.Items[noteNo.ToString()].Selected = true;
            }
        }

        private void FillList()
        {
            if (!this.Created || NoteMap == null) return;

            MapNoteItem selectedItem = null;

            if (MapListVw.SelectedItems.Count > 0)
            {
                selectedItem = (MapNoteItem)MapListVw.SelectedItems[0].Tag;
            }

            MapListVw.Items.Clear();

            foreach (MapNoteItem item in NoteMap)
            {
                ListViewItem lvItem = new ListViewItem(item.TriggerNoteNumber.ToString());
                lvItem.SubItems.Add(item.KeyName);
                lvItem.SubItems.Add(item.OutputNoteNumber.ToString());
                lvItem.Tag = item;
                lvItem.Selected = (selectedItem == item);
                lvItem.Name = item.TriggerNoteNumber.ToString();

                MapListVw.Items.Add(lvItem);
            }

            if (selectedItem == null || MapListVw.SelectedItems.Count == 0)
            {
                if (MapListVw.Items.Count > 0)
                {
                    MapListVw.Items[0].Selected = true;
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            MapNoteDetails dlg = new MapNoteDetails();
            dlg.MapNoteItem = new MapNoteItem()
            {
                KeyName = "New Note Map",
                TriggerNoteNumber = 64,
                OutputNoteNumber = 64
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (NoteMap.Contains(dlg.MapNoteItem.TriggerNoteNumber))
                {
                    NoteMap.Remove(NoteMap[dlg.MapNoteItem.TriggerNoteNumber]);
                }

                NoteMap.Add(dlg.MapNoteItem);
                FillList();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (MapListVw.SelectedItems.Count > 0)
            {
                MapNoteDetails dlg = new MapNoteDetails();
                dlg.MapNoteItem = (MapNoteItem)MapListVw.SelectedItems[0].Tag;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    FillList();
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MapListVw.SelectedItems.Count > 0)
            {
                MapNoteItem item = (MapNoteItem)MapListVw.SelectedItems[0].Tag;

                if (MessageBox.Show(this, 
                    String.Format("Are you sure you want to delete {0}.", item.KeyName), 
                    "Delete a Note Map Item.", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    NoteMap.Remove(item);
                    FillList();
                }
            }
        }

        private void MapListVw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitInfo = MapListVw.HitTest(e.Location);

            if (hitInfo.Item != null)
            {
                hitInfo.Item.Selected = true;

                MapNoteDetails dlg = new MapNoteDetails();
                dlg.MapNoteItem = (MapNoteItem)hitInfo.Item.Tag;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    FillList();
                }
            }
        }

        private void MidiNoteMapperUI_Load(object sender, EventArgs e)
        {
            FillList();
        }
    }
}
