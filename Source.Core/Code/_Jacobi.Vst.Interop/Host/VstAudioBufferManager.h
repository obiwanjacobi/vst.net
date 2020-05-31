#pragma once

#include "UnmanagedArray.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	/// <summary>
	/// The VstAudioBufferManager class manages one continues block of unmanaged memory
	/// to service a number of audio buffers.
	/// </summary>
	/// <remarks>The class is instantiated specifying the number and size of buffers that 
	/// can be accessed using the <see cref="ToArray"/> method. The life time of the 
	/// unmanaged memory that backs up the buffers is coupled to the life time of the
	/// VstAudioBufferManager instance. Calling the <see cref="Dispose"/> method will free
	/// the unmanaged memory.</remarks>
	[System::CLSCompliant(true)]
	public ref class VstAudioBufferManager : System::IDisposable, 
		System::Collections::Generic::IEnumerable<Jacobi::Vst::Core::VstAudioBuffer^>
	{
	public:
		/// <summary>Constructs a new instance for the specified number and size of buffers.</summary>
		/// <param name="bufferCount">The number of buffers.</param>
		/// <param name="bufferSize">The size of a single buffer.</param>
		VstAudioBufferManager(System::Int32 bufferCount, System::Int32 bufferSize);
		/// <summary>Disposes the instance and free's the unmanaged memory.</summary>
		~VstAudioBufferManager();

		/// <summary>Retrieves the buffers objects, one for each buffer.</summary>
		/// <returns>Returns an array of <see cref="Jacobi::Vst::Core::VstAudioBuffer"/> instances.</returns>
		[System::Obsolete("Use the IEnumerable<> interface instead.", false)]
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ ToArray();

		/// <summary>Clears (set all values to 0.0) a single buffer.</summary>
		/// <param name="buffer">The buffer to be cleared. Must not be null.</param>
		void ClearBuffer(Jacobi::Vst::Core::VstAudioBuffer^ buffer);
		/// <summary>Clears all buffers this instance manages.</summary>
		void ClearAllBuffers();

		/// <summary>Gets the number of buffers.</summary>
		property System::Int32 BufferCount { System::Int32 get() { return _bufferCount; } }
		/// <summary>Gets the size of a single buffer.</summary>
		property System::Int32 BufferSize { System::Int32 get() { return _bufferSize; } }

		/// <summary>Retrieves the enumerator object to retrieve <see cref="Jacobi::Vst::Core::VstAudioBuffer"/>'s.</summary>
		virtual System::Collections::Generic::IEnumerator<Jacobi::Vst::Core::VstAudioBuffer^>^ GetEnumerator()
		{
			return _managedBuffers->GetEnumerator();
		}

		/// <summary>Object based enumerator.</summary>
		virtual System::Collections::IEnumerator^ GetObjectEnumerator() sealed = 
			System::Collections::IEnumerable::GetEnumerator
		{
			return GetEnumerator();
		}

	private:
		System::Int32 _bufferCount;
		System::Int32 _bufferSize;

		UnmanagedArray<float> _unmanagedBuffers;
		System::Collections::Generic::List<Jacobi::Vst::Core::VstAudioBuffer^>^ _managedBuffers;

		void ClearBuffer(float* buffer, int bufferSize)
		{
			if(buffer != NULL)
			{
				ZeroMemory(buffer, bufferSize * sizeof(float));
			}
		}

	};

}}}} // namespace Jacobi.Vst.Interop.Host
