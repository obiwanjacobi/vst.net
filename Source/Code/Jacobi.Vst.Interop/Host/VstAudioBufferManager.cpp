#include "StdAfx.h"
#include "VstAudioBufferManager.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	VstAudioBufferManager::VstAudioBufferManager(System::Int32 bufferCount, System::Int32 bufferSize)
	{
		if(bufferCount < 0)
		{
			throw gcnew System::ArgumentOutOfRangeException("bufferCount");
		}

		if(bufferSize <= 0)
		{
			throw gcnew System::ArgumentOutOfRangeException("bufferSize");
		}

		_bufferCount = bufferCount;
		_bufferSize = bufferSize;

		_managedBuffers = gcnew System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioBuffer^>();

		if(_bufferCount > 0)
		{
			// allocate the buffers in one call
			float* pBuffer = _unmanagedBuffers.GetArray(bufferCount * bufferSize);

			for(int n = 0; n < bufferCount; n++)
			{
				_managedBuffers->Add(gcnew Jacobi::Vst::Core::VstAudioBuffer(pBuffer, bufferSize, true));
				pBuffer += n * bufferSize;
			}
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
			throw gcnew System::ArgumentException("Buffer size does not match this manager instance.", "buffer");
		}
		
		Jacobi::Vst::Core::IDirectBufferAccess32^ directBuf = (Jacobi::Vst::Core::IDirectBufferAccess32^)buffer;

		float* lowerBound = _unmanagedBuffers.GetArray();
		float* upperBound = lowerBound + (_bufferSize * _bufferCount);
		float* pBuffer = directBuf->Buffer;

		// check if the unmanaged buffer matches the range of our _unamangedBuffers array.
		if(lowerBound == NULL ||
			!(lowerBound <= pBuffer && pBuffer < upperBound))
		{
			throw gcnew System::ArgumentException("Specified buffer is not managed by this instance.", "buffer");
		}

		ClearBuffer(directBuf->Buffer, directBuf->SampleCount);
	}

	void VstAudioBufferManager::ClearAllBuffers()
	{
		ClearBuffer(_unmanagedBuffers.GetArray(), _unmanagedBuffers.GetByteLength());
	}

}}}} // namespace Jacobi.Vst.Interop.Host
