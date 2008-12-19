#include "StdAfx.h"
#include "VstAudioBufferManager.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	VstAudioBufferManager::VstAudioBufferManager(System::Int32 bufferCount, System::Int32 bufferSize)
	{
		if(bufferCount <= 0)
		{
			throw gcnew System::ArgumentOutOfRangeException("bufferCount");
		}

		if(bufferSize <= 0)
		{
			throw gcnew System::ArgumentOutOfRangeException("bufferSize");
		}

		_bufferCount = bufferCount;
		_bufferSize = bufferSize;

		// allocate the buffers in one call
		float* pBuffer = _unmanagedBuffers.GetArray(bufferCount * bufferSize);
		_managedBuffers = gcnew System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioBuffer^>();

		for(int n = 0; n < bufferCount; n++)
		{
			_managedBuffers->Add(gcnew Jacobi::Vst::Core::VstAudioBuffer(pBuffer, bufferSize, true));
			pBuffer += n * bufferSize;
		}
	}

	VstAudioBufferManager::~VstAudioBufferManager()
	{
		this->!VstAudioBufferManager();
	}

	VstAudioBufferManager::!VstAudioBufferManager()
	{
	}

	array<Jacobi::Vst::Core::VstAudioBuffer^>^ VstAudioBufferManager::ToArray()
	{
		return _managedBuffers->ToArray();
	}

	void VstAudioBufferManager::ClearBuffer(Jacobi::Vst::Core::VstAudioBuffer^ buffer)
	{
		if(buffer == nullptr)
		{
			throw gcnew System::ArgumentNullException("buffer");
		}

		if(buffer->SampleCount != _bufferSize)
		{
			throw gcnew System::ArgumentException("Buffer size does not match this manager.", "buffer");
		}
		
		Jacobi::Vst::Core::IDirectBufferAccess32^ directBuf = (Jacobi::Vst::Core::IDirectBufferAccess32^)buffer;

		// TODO: check if the unmanaged buffer matches the range of our _unamangedBuffers array.

		ZeroMemory(directBuf->Buffer, directBuf->SampleCount);
	}

	void VstAudioBufferManager::ClearAllBuffers()
	{
		ZeroMemory(_unmanagedBuffers.GetArray(), _unmanagedBuffers.GetByteLength());
	}

}}}} // namespace Jacobi.Vst.Interop.Host
