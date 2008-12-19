#pragma once

#include "UnmanagedArray.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	public ref class VstAudioPrecisionBufferManager : System::IDisposable
	{
	public:
		VstAudioPrecisionBufferManager(System::Int32 bufferCount, System::Int32 bufferSize);
		~VstAudioPrecisionBufferManager();
		!VstAudioPrecisionBufferManager();

		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ ToArray();

		void ClearBuffer(Jacobi::Vst::Core::VstAudioPrecisionBuffer^ buffer);
		void ClearAllBuffers();

		property System::Int32 BufferCount { System::Int32 get() { return _bufferCount; } }
		property System::Int32 BufferSize { System::Int32 get() { return _bufferSize; } }

	private:
		System::Int32 _bufferCount;
		System::Int32 _bufferSize;

		UnmanagedArray<double> _unmanagedBuffers;
		System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ _managedBuffers;
	};

}}}} // namespace Jacobi.Vst.Interop.Host
