#pragma once

ref class TimeCriticalScope : public System::IDisposable
{
public:
	TimeCriticalScope(void);
	!TimeCriticalScope(void);
	~TimeCriticalScope(void);

private:
	System::Runtime::GCLatencyMode _originalMode;
};
