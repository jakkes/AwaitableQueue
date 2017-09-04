# AwaitableQueue
Super tiny wrapper of the standard Queue class. Added a method `DequeueAsync` that will either Dequeue as normal or wait until an element in enqueued.

## Installation
### .NET CLI
`dotnet add package AwaitableQueue`

### Package Manager
`Install-Package AwaitableQueue`

## Usage
```
AwaitableQueue<int> q = new AwaitableQueue<int>();

addItemToQueueInTenSeconds();

// Wait for an object to be added. If queue is not empty, dequeue as normal.
int re = await q.DequeueAsync();

```