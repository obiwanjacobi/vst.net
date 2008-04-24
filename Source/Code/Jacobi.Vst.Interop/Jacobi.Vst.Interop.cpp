// Jacobi.Vst.Interop.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

// main experted method called by host to create the plugin
AEffect* VSTMain (audioMasterCallback hostCallback)
{
	// Loader:
	// load managed plugin type from external assembly
	// create and initialize plugin instance
	// query capabilities and overrides of core infra parts

	// Factory:
	// create AEffect struct
	// linkup procs to core
	// copy managed props to AEffect struct

	return NULL;
}