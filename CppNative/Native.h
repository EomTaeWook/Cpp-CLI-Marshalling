#pragma once
#include <string>
//C++ Native �̸� Lib�� ���尡 �Ǿ����.
class Native
{
public:
	typedef void(__stdcall *OutGoingCallback)(std::string);
public:
	Native();
	~Native();
private:
	//Wrapping �Ǵ� C++/cli �Լ� ������ ����
	OutGoingCallback _onNotify;
public:
	void Init(OutGoingCallback onNotify);
	void InCommingMessage(std::string message);
	void Stop();
private:
	void OutGoingMessage(std::string message);
};

