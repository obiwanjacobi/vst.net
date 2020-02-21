#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The TimeCriticalScope sets the Latency Mode of the GC to 'LowLatency' during the lifetime of the instance (scope).
/// </summary>
ref class TimeCriticalScope : public System::IDisposable
{
public:
	/// <summary>
	/// Constructs a new instance and sets the latence mode of the GC.
	/// </summary>
	TimeCriticalScope(void)
	{
		_originalMode = System::Runtime::GCSettings::LatencyMode;
		System::Runtime::GCSettings::LatencyMode = System::Runtime::GCLatencyMode::LowLatency;
	}

	/// <summary>
	/// Restores the latency mode of the GC to its original value.
	/// </summary>
	~TimeCriticalScope(void)
	{
		System::Runtime::GCSettings::LatencyMode = _originalMode;
	}

private:
	System::Runtime::GCLatencyMode _originalMode;
};

}}} // Jacobi::Vst::Interop