using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType
{
    class Program
    {
        static void Main(string[] args)
        {
            var entry = new Entry();
            entry.Start();
            Console.ReadKey();
            entry.Dispose();//thread destroy check
            Console.WriteLine("Press key Program Close");
            Console.ReadKey();
        }
    }
}
