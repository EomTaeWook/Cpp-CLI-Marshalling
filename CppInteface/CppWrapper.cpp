#include "CppWrapper.h"
#include <msclr/marshal_cppstd.h>
void CppWrapper::Run()
{
	
}
void CppWrapper::Stop()
{
	_native->Stop();
}
void CppWrapper::InCommingMessage(System::String ^inGoingMessage)
{
	std::string message = msclr::interop::marshal_as<std::string>(inGoingMessage);
	_native->InCommingMessage(message);
}
//Native 단에서 넘겨받은 Message를 구독하고 있는 모든 애들한테 Notify
void CppWrapper::OutGoingMessage(std::string outGoingMessage)
{
	System::String^ message = gcnew System::String(outGoingMessage.c_str());
	OnNotify(this, message);
}

void CppWrapper::Init()
{
	auto cb = gcnew OnNotifyCallback(this, &CppWrapper::OutGoingMessage);
	System::IntPtr ptr = System::Runtime::InteropServices::Marshal::GetFunctionPointerForDelegate(cb);
	_native->Init(static_cast<Native::OutGoingCallback>(ptr.ToPointer()));
}
