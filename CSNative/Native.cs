using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeCS
{
    public class Native : CS.CSInterface
    {
        public event Action<object, string> OnNotify;
        private bool _disposed;
        private void Dispose(bool isDispose)
        {
            _disposed = isDispose;
        }
        public void Dispose()
        {
            if (_disposed)
                return;
            Dispose(true);
        }

        public void Init()
        {
            OutGoinMessage("C# Init Complete");
        }
        public void Run()
        {
            
        }
        public void InCommingMessage(string message)
        {
            Console.WriteLine($"[C#] Notify InCommingMessage :: {message}");
        }
        void OutGoinMessage(string message)
        {
            //Console.WriteLine("C# OutGoinMessage");
            OnNotify?.Invoke(this, message);
        }
        public void Stop()
        {
            Console.WriteLine("Stop C# Native Class");
        }
    }
}
