using System;
using Xunit;
using AwaitableQueue;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitableQueue.Test
{
    public class AwaitableQueueTest
    {
        AwaitableQueue<int> q = new AwaitableQueue<int>();

        [Fact]
        public void Enqueue()
        {
            q.Enqueue(2);
            q.Dequeue();
        }

        [Fact]
        public async void TestAwait()
        {
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                q.Enqueue(2);
            });

            Assert.True(await q.DequeueAsync() == 2);
        }

        [Fact]
        public async void Test2Awaits()
        {
            var t1 = Assert.ThrowsAsync<TestException>(async () =>
            {
                int a = await q.DequeueAsync();
                Console.WriteLine(a);
                throw new TestException();
            });

            var t2 = Assert.ThrowsAsync<TestException>(async () =>
            {
                int a = await q.DequeueAsync();
                Console.WriteLine(a);
                throw new TestException();
            });

            q.Enqueue(2);

            var r1 = await t1;

            q.Enqueue(2);

            var r2 = await t2;
        }

        private async Task<int> deq()
        {
            return await q.DequeueAsync();
        }
    }

    public class TestException : Exception { }
}
