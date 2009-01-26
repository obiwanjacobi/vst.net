#include "StdAfx.h"
#include "TimeCriticalScope.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

// starts a time critical scope
TimeCriticalScope::TimeCriticalScope(void)
{
	_originalMode = System::Runtime::GCSettings::LatencyMode;
	System::Runtime::GCSettings::LatencyMode = System::Runtime::GCLatencyMode::LowLatency;
}

// ends a time critical scope
TimeCriticalScope::~TimeCriticalScope(void)
{
	this->!TimeCriticalScope();
}

// ends a time critical scope
TimeCriticalScope::!TimeCriticalScope(void)
{
	System::Runtime::GCSettings::LatencyMode = _originalMode;
}

}}} // Jacobi::Vst::Interop