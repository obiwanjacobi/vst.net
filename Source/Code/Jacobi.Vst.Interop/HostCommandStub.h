#pragma once

ref class HostCommandStub
{
public:

internal:
	HostCommandStub(AEffect* pluginInfo, audioMasterCallback hostCallback);

private:
	AEffect* _pluginInfo;
	audioMasterCallback _hostCallback;
};
