#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Properties {

	ref class Resources
	{
	public:
		static property System::String^ VstAudioBufferManager_InvalidBufferSize
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstAudioBufferManager_InvalidBufferSize", Culture);
			}
		}

		static property System::String^ VstAudioBufferManager_BufferNotOwned
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstAudioBufferManager_BufferNotOwned", Culture);
			}
		}

		static property System::String^ VstPluginCommandStub_SampleCountMismatch
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstPluginCommandStub_SampleCountMismatch", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_AlreadyInitialized
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_AlreadyInitialized", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_LoadPluginFailed
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_LoadPluginFailed", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_EntryPointNotFound
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_EntryPointNotFound", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_PluginReturnedNull
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_PluginReturnedNull", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_MagicNumberMismatch
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_MagicNumberMismatch", Culture);
			}
		}

		static property System::String^ VstUnmanagedPluginContext_VstVersionMismatch
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstUnmanagedPluginContext_VstVersionMismatch", Culture);
			}
		}

		static property System::String^ VstManagedPluginContext_PluginCommandStubNotFound
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstManagedPluginContext_PluginCommandStubNotFound", Culture);
			}
		}

		static property System::String^ HostCommandStub_VstFileSelectAlreadyInitialized
		{
			System::String^ get()
			{
				return ResourceManager->GetString("HostCommandStub_VstFileSelectAlreadyInitialized", Culture);
			}
		}

		static property System::String^ HostCommandStub_VstFileSelectAlreadyDisposed
		{
			System::String^ get()
			{
				return ResourceManager->GetString("HostCommandStub_VstFileSelectAlreadyDisposed", Culture);
			}
		}

		static property System::String^ HostCommandStub_NotInitialized
		{
			System::String^ get()
			{
				return ResourceManager->GetString("HostCommandStub_NotInitialized", Culture);
			}
		}

		static property System::String^ VstInteropMain_CouldNotCreatePluginCmdStub
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstInteropMain_CouldNotCreatePluginCmdStub", Culture);
			}
		}

		static property System::String^ VstInteropMain_GetPluginInfoNull
		{
			System::String^ get()
			{
				return ResourceManager->GetString("VstInteropMain_GetPluginInfoNull", Culture);
			}
		}

		//---------------------------------------------------------------------

		static property System::Resources::ResourceManager^ ResourceManager
		{
			System::Resources::ResourceManager^ get()
			{
				if(_rescMgr == nullptr)
				{
					_rescMgr = gcnew System::Resources::ResourceManager("JacobiVstInterop.Properties.Resources", 
						System::Reflection::Assembly::GetExecutingAssembly());
				}

				return _rescMgr;
			}
		}

		static property System::Globalization::CultureInfo^ Culture;

	private:
		static System::Resources::ResourceManager^ _rescMgr;
	};

}}}}	// Jacobi::Vst::Interop::Properties