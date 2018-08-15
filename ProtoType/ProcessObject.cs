using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProtoType
{
    public class ProcessObject
    {
        public object DllProcess { get; set; }
        public bool Started { get; set; }
        public CancellationTokenSource CancellationToken { get; set; }

    }
}
