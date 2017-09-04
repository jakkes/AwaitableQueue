using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitableQueue
{
    public class AwaitableQueue<T> : Queue<T>
    {
        public AwaitableQueue() : base() { }
        public AwaitableQueue(int capacity) : base(capacity) { }
        public AwaitableQueue(IEnumerable<T> collection ) : base(collection) { }
        private AutoResetEvent _enqueued = new AutoResetEvent(false);
        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            _enqueued.Set();
        }
        public async Task<T> DequeueAsync()
        {
            if (Count > 0)
                return Dequeue();

            await Task.Run(() =>
            {
                _enqueued.WaitOne();
            });
            return Dequeue();
        }
    }
}
