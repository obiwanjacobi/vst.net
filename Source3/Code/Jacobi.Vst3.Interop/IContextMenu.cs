using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IContextMenu)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IContextMenu
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
    	Int32 GetItemCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 GetItem(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref ContextMenuItem item,
            [MarshalAs(UnmanagedType.Interface), In, Out] ref IContextMenuTarget target);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 AddItem(
            [MarshalAs(UnmanagedType.Struct), In] ref ContextMenuItem item,
            [MarshalAs(UnmanagedType.Interface), In] IContextMenuTarget target);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 RemoveItem(
            [MarshalAs(UnmanagedType.Struct), In] ref ContextMenuItem item,
            [MarshalAs(UnmanagedType.Interface), In] IContextMenuTarget target);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 Popup(
            [MarshalAs(UnmanagedType.I4), In] Int32 x,
            [MarshalAs(UnmanagedType.I4), In] Int32 y);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct ContextMenuItem
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public String Name;									///< Name of the item

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Tag;										///< Identifier tag of the item

        [MarshalAs(UnmanagedType.I4)]
        public ItemFlags Flags;									///< Flags of the item

        public enum ItemFlags
        {
            kIsSeparator = 1 << 0,					///< Item is a separator
            kIsDisabled = 1 << 1,					///< Item is disabled
            kIsChecked = 1 << 2,					///< Item is checked
            kIsGroupStart = 1 << 3 | kIsDisabled,		///< Item is a group start (like sub folder)
            kIsGroupEnd = 1 << 4 | kIsSeparator,	///< Item is a group end
        }
    }
}
