#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

ref class TimeCriticalScope : public System::IDisposable
{
public:
	TimeCriticalScope(void);
	!TimeCriticalScope(void);
	~TimeCriticalScope(void);

private:
	System::Runtime::GCLatencyMode _originalMode;
};

}}} // Jacobi::Vst::Interop