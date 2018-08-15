#pragma once
#include "../Native/Native.h"
//C++/Cli .net ȯ�濡�� �����Ǵ� C++
public ref class CppWrapper : Cpp::CppInterface
{
public:
	//Cpp Native ������ �Լ� �����͸� �Ѱ��ֱ� ���� �븮��
	//OutGoingMessage(std::string message);�� ��Ī�ȴ�.
	delegate void OnNotifyCallback(std::string message);
public:
	CppWrapper();
	virtual ~CppWrapper();
private:
	void OutGoingMessage(std::string outGoingMessage);
private:
	Native* _native;
public:
	// CppInterface��(��) ���� ��ӵ�
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

