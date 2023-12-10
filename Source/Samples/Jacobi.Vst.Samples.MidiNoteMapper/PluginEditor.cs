namespace Jacobi.Vst.Samples.MidiNoteMapper;

using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Common;
using System;

/// <summary>
/// Implements the custom UI editor for the plugin.
/// </summary>
internal sealed class PluginEditor : IVstPluginEditor
{
    private readonly MidiProcessor _midiProcessor;
    private readonly MapNoteItemList _noteMap;
    private readonly WinFormsControlWrapper<UI.MidiNoteMapperView> _uiWrapper =
        new WinFormsControlWrapper<UI.MidiNoteMapperView>();

    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    /// <param name="plugin">Must not be null.</param>
    public PluginEditor(MidiProcessor midiProcessor, MapNoteItemList noteMap)
    {
        _midiProcessor = midiProcessor ?? throw new ArgumentNullException(nameof(midiProcessor));
        _noteMap = noteMap ?? throw new ArgumentNullException(nameof(noteMap));
    }

    #region IVstPluginEditor Members

    public System.Drawing.Rectangle Bounds
    {
        get { return _uiWrapper.Bounds; }
    }

    public void Close()
    {
        _uiWrapper.Close();
    }

    public bool KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
    {
        // no-op
        return false;
    }

    public bool KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
    {
        // no-op
        return false;
    }

    public VstKnobMode KnobMode { get; set; }

    public void Open(IntPtr hWnd)
    {
        _uiWrapper.SafeInstance.NoteMap = _noteMap;
        _uiWrapper.SafeInstance.NoteOnEvents = _midiProcessor.NoteOnEvents;
        _uiWrapper.Open(hWnd);
    }

    public void ProcessIdle()
    {
        _uiWrapper.SafeInstance.ProcessIdle();
    }

    #endregion
}
