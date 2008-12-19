#pragma once

#include "UnmanagedArray.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	public ref class VstAudioBufferManager : System::IDisposable
	{
	public:
		VstAudioBufferManager(System::Int32 bufferCount, System::Int32 bufferSize);
		~VstAudioBufferManager();
		!VstAudioBufferManager();

		array<Jacobi::Vst::Core::VstAudioBuffer^>^ ToArray();

		void ClearBuffer(Jacobi::Vst::Core::VstAudioBuffer^ buffer);
		void ClearAllBuffers();

		property System::Int32 BufferCount { System::Int32 get() { return _bufferCount; } }
		property System::Int32 BufferSize { System::Int32 get() { return _bufferSize; } }

	private:
		System::Int32 _bufferCount;
		System::Int32 _bufferSize;

		UnmanagedArray<float> _unmanagedBuffers;
		System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioBuffer^>^ _managedBuffers;
	};

}}}} // namespace Jacobi.Vst.Interop.Host
