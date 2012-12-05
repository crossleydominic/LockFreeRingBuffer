using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockFreeRingBuffer.Test;

namespace LockFreeRingBuffer.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            Test.PerformanceTests t = new PerformanceTests();
            t.NormalFencePerfTest();

            Console.ReadLine();
        }
    }
}
