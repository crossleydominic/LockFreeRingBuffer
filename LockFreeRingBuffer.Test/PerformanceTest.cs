using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LockFreeRingBuffer.Core;
using NUnit.Framework;

namespace LockFreeRingBuffer.Test
{
    [TestFixture]
    public class PerformanceTests
    {
        [Test]
        public void NormalFencePerfTest()
        {
            long numberOfItems = 10000000;

            ManualResetEvent evt = new ManualResetEvent(false);

            RingBuffer<long> buffer = new RingBuffer<long>(1000);

            Thread producer = new Thread(() =>
            {
                evt.WaitOne();

                long count = 0;

                while (count < numberOfItems)
                {
                    bool wasPushed = buffer.Push(1);

                    if (wasPushed)
                    {
                        count++;
                    }
                }
            });

            Thread consumer = new Thread(() =>
            {
                evt.WaitOne();

                long count = 0;

                while (count < numberOfItems)
                {
                    long popped = 0;
                    bool wasPopped = buffer.Pop(ref popped);

                    if (wasPopped)
                    {
                        count += popped;
                    }
                }

            });

            producer.Start();
            consumer.Start();

            Thread.Sleep(1000);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            evt.Set();

            producer.Join();
            consumer.Join();

            stopwatch.Stop();

            Console.WriteLine(string.Format("Total time taken: {0}ms", stopwatch.ElapsedMilliseconds));

        }
    }
}
