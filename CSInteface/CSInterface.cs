using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS
{
    public interface CSInterface : IDisposable
    {
        event Action<object, string> OnNotify;
        void Init();
        void Run();
        void InCommingMessage(string message);
        void Stop();
    }
}
