/*
 * audioMasterGetTime
 * audioMasterProcessEvents
 * audioMasterIOChanged
 * audioMasterSizeWindow
 * audioMasterGetSampleRate
 * audioMasterGetBlockSize
 * audioMasterGetInputLatency
 * audioMasterGetOutputLatency
 * audioMasterGetCurrentProcessLevel
 * audioMasterGetAutomationState
 * audioMasterOfflineStart
 * audioMasterOfflineRead
 * audioMasterOfflineWrite
 * audioMasterOfflineGetCurrentPass
 * audioMasterOfflineGetCurrentMetaPass
 * audioMasterGetVendorString
 * audioMasterGetProductString
 * audioMasterGetVendorVersion
 * audioMasterVendorSpecific
 * audioMasterCanDo
 * audioMasterGetLanguage
 * audioMasterGetDirectory
 * audioMasterUpdateDisplay
 * audioMasterBeginEdit
 * audioMasterEndEdit
 * audioMasterOpenFileSelector
 * audioMasterCloseFileSelector
 * 
 */

namespace Jacobi.Vst.Framework
{
    public interface IVstHost : IExtensible
    {
        VstProductInfo ProductInfo { get; }

        VstHostCapabilities Capabilities { get; }
    }
}
