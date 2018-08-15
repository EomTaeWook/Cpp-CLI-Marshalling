#pragma once
#include "../Native/Native.h"
//C++/Cli .net 환경에서 구동되는 C++
public ref class CppWrapper : Cpp::CppInterface
{
public:
	//Cpp Native 단으로 함수 포인터를 넘겨주기 위한 대리자
	//OutGoingMessage(std::string message);과 매칭된다.
	delegate void OnNotifyCallback(std::string message);
public:
	CppWrapper();
	virtual ~CppWrapper();
private:
	void OutGoingMessage(std::string outGoingMessage);
private:
	Native* _native;
public:
	// CppInterface을(를) 통해 상속됨
	virtual event Cpp::CppInterface::OnNotifyHandler ^ OnNotify;
	virtual void Init();
	virtual void Run();
	virtual void Stop();
	virtual void InCommingMessage(System::String ^inCommingMessage);
};

inline CppWrapper::CppWrapper()
{
	_native = new Native();
}
inline CppWrapper::~CppWrapper()
{
	delete _native;
}

