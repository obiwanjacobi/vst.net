#include "StdAfx.h"
#include "VstAudioPrecisionBufferManager.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	VstAudioPrecisionBufferManager::VstAudioPrecisionBufferManager(System::Int32 bufferCount, System::Int32 bufferSize)
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
		double* pBuffer = _unmanagedBuffers.GetArray(bufferCount * bufferSize);
		_managedBuffers = gcnew System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>();

		for(int n = 0; n < bufferCount; n++)
		{
			_managedBuffers->Add(gcnew Jacobi::Vst::Core::VstAudioPrecisionBuffer(pBuffer, bufferSize, true));
			pBuffer += n * bufferSize;
		}
	}

	VstAudioPrecisionBufferManager::~VstAudioPrecisionBufferManager()
	{
		this->!VstAudioPrecisionBufferManager();
	}

	VstAudioPrecisionBufferManager::!VstAudioPrecisionBufferManager()
	{
	}

	array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ VstAudioPrecisionBufferManager::ToArray()
	{
		return _managedBuffers->ToArray();
	}

	void VstAudioPrecisionBufferManager::ClearBuffer(Jacobi::Vst::Core::VstAudioPrecisionBuffer^ buffer)
	{
		if(buffer == nullptr)
		{
			throw gcnew System::ArgumentNullException("buffer");
		}

		if(buffer->SampleCount != _bufferSize)
		{
			throw gcnew System::ArgumentException("Buffer size does not match this manager.", "buffer");
		}
		
		Jacobi::Vst::Core::IDirectBufferAccess64^ directBuf = (Jacobi::Vst::Core::IDirectBufferAccess64^)buffer;

		// TODO: check if the unmanaged buffer matches the range of our _unamangedBuffers array.

		ZeroMemory(directBuf->Buffer, directBuf->SampleCount);
	}

	void VstAudioPrecisionBufferManager::ClearAllBuffers()
	{
		ZeroMemory(_unmanagedBuffers.GetArray(), _unmanagedBuffers.GetByteLength());
	}

}}}} // namespace Jacobi.Vst.Interop.Host
