#include "StdAfx.h"
#include "HostCommandStub.h"

HostCommandStub::HostCommandStub(AEffect* pluginInfo, audioMasterCallback hostCallback)
{
	_pluginInfo = pluginInfo;
	_hostCallback = hostCallback;
}

//bool HostCommandStub::Process()
//{
//	switch()
//	{
//	case audioMasterAutomate:
//		break;
//	case audioMasterVersion:
//		break;
//	case audioMasterCurrentId:
//		break;
//	case audioMasterIdle:
//		break;
//	case audioMasterGetTime:
//		break;
//	case audioMasterProcessEvents:
//		break;
//	case audioMasterIOChanged:
//		break;
//	case audioMasterSizeWindow:
//		break;
//	case audioMasterGetSampleRate:
//		break;
//	case audioMasterGetBlockSize:
//		break;
//	case audioMasterGetInputLatency:
//		break;
//	case audioMasterGetOutputLatency:
//		break;
//	case audioMasterGetCurrentProcessLevel:
//		break;
//	case audioMasterGetAutomationState:
//		break;
//	case audioMasterOfflineStart:
//		break;
//	case audioMasterOfflineRead:
//		break;
//	case audioMasterOfflineWrite:
//		break;
//	case audioMasterOfflineGetCurrentPass:
//		break;
//	case audioMasterOfflineGetCurrentMetaPass:
//		break;
//	case audioMasterGetVendorString:
//		break;
//	case audioMasterGetProductString:
//		break;
//	case audioMasterGetVendorVersion:
//		break;
//	case audioMasterCanDo:
//		break;
//	case audioMasterGetLanguage:
//		break;
//	case audioMasterGetDirectory:
//		break;
//	case audioMasterUpdateDisplay:
//		break;
//	case audioMasterBeginEdit:
//		break;
//	case audioMasterEndEdit:
//		break;
//	case audioMasterOpenFileSelector:
//		break;
//	case audioMasterCloseFileSelector:
//		break;
//	default:
//		break;
//	}
//
//	return true;
//}