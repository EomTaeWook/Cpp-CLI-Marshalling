#include "Native.h"
#include <stdio.h>
Native::Native()
{
}
Native::~Native()
{
}
void Native::Init(OutGoingCallback onNotify)
{
	_onNotify = onNotify;
	OutGoingMessage("Cpp UnManaged class Init Complete");
}
void Native::OutGoingMessage(std::string outGoingMessage)
{
	//printf("Cpp UnManaged class -> Managed class -> C# Outgoing Message\n");
	_onNotify(outGoingMessage);
}
void Native::InCommingMessage(std::string inGoingMessage)
{
	//printf("C#-> Managed class -> Cpp UnManaged class ->  ingoing Message\n");
	printf("[Cpp] Notify InCommingMessage :: %s\n", inGoingMessage.c_str());
}
void Native::Stop()
{
	printf("Cpp Native Stop\n");
}