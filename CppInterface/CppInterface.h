#pragma once
namespace Cpp
{
	public interface class CppInterface
	{
	public:
		delegate void OnNotifyHandler(System::Object^ sender, System::String^ message);
	public:
		void Init();
		void Run();
		void Stop();
		void InCommingMessage(System::String^ inCommingMessage);
		event OnNotifyHandler^ OnNotify;
	};
}
