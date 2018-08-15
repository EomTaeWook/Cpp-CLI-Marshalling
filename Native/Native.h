#pragma once
#include <string>
//C++ Native 이며 Lib로 빌드가 되어야함.
class Native
{
public:
	typedef void(__stdcall *OutGoingCallback)(std::string);
public:
	Native();
	~Native();
private:
	//Wrapping 되는 C++/cli 함수 포인터 저장
	OutGoingCallback _onNotify;
public:
	void Init(OutGoingCallback onNotify);
	void InCommingMessage(std::string message);
	void Stop();
private:
	void OutGoingMessage(std::string message);
};

