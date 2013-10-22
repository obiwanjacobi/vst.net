// Jacobi.Vst3.TestApp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "AssemblyLoader.h"

using namespace Steinberg;

void Test_IPluginFactory(IPluginFactory* pFactory)
{
	PFactoryInfo factoryInfo;
	tresult result = pFactory->getFactoryInfo(&factoryInfo);

	if (result != kResultOk)
	{
		printf("Error retrieving factory info.\n");
		return;
	}

	printf("Factory: %s - %s - %s\n", factoryInfo.vendor, factoryInfo.url, factoryInfo.email);

	int nrOfClasses = pFactory->countClasses();
	printf("The factory supports %i classes.\n", nrOfClasses);

	for(int i = 0; i < nrOfClasses; i++)
	{
		PClassInfo classInfo;
		result = pFactory->getClassInfo(i, &classInfo);

		if (result != kResultOk)
		{
			printf("Error retrieving class info.\n");
			return;
		}

		printf("Class: %s - %s\n", classInfo.name, classInfo.category);

		IPluginBase* pPluginBase = NULL;
		result = pFactory->createInstance(classInfo.cid, IPluginBase::iid, (void**)&pPluginBase);

		if (result != kResultOk)
		{
			printf("Error creating component instance.\n");
			return;
		}

		result = pPluginBase->initialize(NULL);

		if (result != kResultOk)
		{
			pPluginBase->release();
			printf("Plugin component could not initialize.\n");
			return;
		}

		result = pPluginBase->terminate();
		pPluginBase->release();
	}
}

void Test_IPluginFactory2(IPluginFactory* pFactory)
{
	IPluginFactory2* pFactory2 = NULL;

	tresult result = pFactory->queryInterface(IPluginFactory2::iid, (void**)&pFactory2);

	if (result != kResultOk)
	{
		printf("IPluginFactory2 interface was not found (not an error).\n");
		return;
	}

	int nrOfClasses = pFactory2->countClasses();

	for(int i = 0; i < nrOfClasses; i++)
	{
		PClassInfo2 classInfo;
		result = pFactory2->getClassInfo2(i, &classInfo);

		if (result != kResultOk)
		{
			printf("Error retrieving class 2 info.\n");
			return;
		}

		printf("Class2: %s - %s - %s - %s\n", classInfo.name, classInfo.category, classInfo.vendor, classInfo.version);
	}
}

void Test_IPluginFactory3(IPluginFactory* pFactory)
{
	IPluginFactory3* pFactory3 = NULL;

	tresult result = pFactory->queryInterface(IPluginFactory3::iid, (void**)&pFactory3);

	if (result != kResultOk)
	{
		printf("IPluginFactory3 interface was not found (not an error).\n");
		return;
	}

	int nrOfClasses = pFactory3->countClasses();

	for(int i = 0; i < nrOfClasses; i++)
	{
		PClassInfoW classInfo;
		result = pFactory3->getClassInfoUnicode(i, &classInfo);

		if (result != kResultOk)
		{
			printf("Error retrieving class 3 info.\n");
			return;
		}

		printf("Class3: %ws - %s - %ws - %ws\n", classInfo.name, classInfo.category, classInfo.vendor, classInfo.version);
	}
}


int _tmain(int argc, _TCHAR* argv[])
{
	AssemblyLoader* loader = NULL;
	
	if (argc == 2)
	{
		loader = new AssemblyLoader(argv[1]);
	}
	else
	{
		loader = new AssemblyLoader();
	}

	GetFactoryProc proc = loader->GetPluginFactoryProcedure();

	if (proc == NULL)
	{
		printf("Could not load TestPlugin or no export-method was found.\n");
		return -1;
	}

	/*SHOW_DEFINE(INIT_CLASS_IID);
	SHOW_DEFINE(PLUGIN_API);
	SHOW_DEFINE(WINDOWS);
	SHOW_DEFINE(COM_COMPATIBLE);*/

	// call procedure to retrieve the factory interface
	IPluginFactory* pFactory = proc();

	if (pFactory == NULL)
	{
		printf("The GetPluginFactory method returned NULL.\n");
		return -2;
	}

	Test_IPluginFactory(pFactory);
	Test_IPluginFactory2(pFactory);
	Test_IPluginFactory3(pFactory);



	return 0;
}

