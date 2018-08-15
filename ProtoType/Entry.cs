using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ProtoType
{
    public class Entry :IDisposable
    {
        private ConcurrentDictionary<string, ProcessObject> _processes;
        private bool _disposed;
        public Entry()
        {
            _processes = new ConcurrentDictionary<string, ProcessObject>();
        }
        protected void Dispose(bool isDispose)
        {
            foreach(var p in _processes)
            {
                p.Value.CancellationToken.Cancel();
            }
            _processes.Clear();
            _disposed = isDispose;
        }
        public void Dispose()
        {
            if (_disposed)
                return;
            Dispose(true);
        }
        //각 DLL로 부터 Notify가 들어오는 구간
        private void OnNotify(object sender, string message)
        {
            Console.WriteLine($"[Center] Notify Recive : {message}");
            //등록되어 있는 Dll 클래스에게 Notify 전달
            foreach (var item in _processes)
            {
                var method = item.Value.DllProcess.GetType().GetMethod("InCommingMessage", BindingFlags.Public | BindingFlags.Instance);
                method?.Invoke(item.Value.DllProcess, new object[] { message });
            }
        }
        private void ProcessStart(object process)
        {
            ProcessObject state = process as ProcessObject;
            var stopMethod = state.DllProcess.GetType().GetMethod("Stop", BindingFlags.Instance | BindingFlags.Public);
            while (!state.CancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (state.Started)
                        continue;

                    var initMethod = state.DllProcess.GetType().GetMethod("Init", BindingFlags.Instance | BindingFlags.Public);
                    initMethod?.Invoke(state.DllProcess, null);

                    var runMethod = state.DllProcess.GetType().GetMethod("Run", BindingFlags.Instance | BindingFlags.Public);
                    runMethod?.Invoke(state.DllProcess, null);

                    state.Started = true;
                }
                catch(Exception ex)
                {
                    stopMethod?.Invoke(state.DllProcess, null);
                    state.Started = false;
                    Trace.WriteLine(ex.Message);
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
            if (state.DllProcess is CS.CSInterface)
            {
                var p = state.DllProcess as CS.CSInterface;
                p.OnNotify -= OnNotify;
            }
            else if (state.DllProcess is Cpp.CppInterface)
            {
                var p = state.DllProcess as Cpp.CppInterface;
                p.OnNotify -= OnNotify;
            }
            stopMethod? .Invoke(state.DllProcess, null);
            Console.WriteLine($"Thread Close");
        }
        public void Start()
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory).Where(r=>r.EndsWith("dll")).ToList();
            foreach(var file in files)
            {
                var assem = Assembly.LoadFile(file);
                var types = assem.GetExportedTypes().Where(r => r.IsClass &&
                                                                r.GetInterfaces().Any(i => i.Equals(typeof(Cpp.CppInterface))) ||
                                                                r.GetInterfaces().Any(i => i.Equals(typeof(CS.CSInterface)))
                                                                ).ToList();
                foreach (var type in types)
                {
                    var process = new ProcessObject();
                    process.CancellationToken = new CancellationTokenSource();

                    process.DllProcess = Activator.CreateInstance(type);
                    if (process.DllProcess is CS.CSInterface)
                    {
                        var p = process.DllProcess as CS.CSInterface;
                        p.OnNotify += OnNotify; //옵저버 패턴 생각하면 됨
                    }
                    else if (process.DllProcess is Cpp.CppInterface)
                    {
                        var p = process.DllProcess as Cpp.CppInterface;
                        p.OnNotify += OnNotify; //옵저버 패턴 생각하면 됨
                    }
                    _processes.TryAdd(type.Name, process);
                }
            }
            foreach (var process in _processes)
            {
                ThreadPool.QueueUserWorkItem(ProcessStart, process.Value);
            }
        }
    }
}
