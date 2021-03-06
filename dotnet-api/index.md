---
outputFileName: index.html
---

# Overview

The .NET Client API communicates with Event Store over TCP, using length-prefixed serialised protocol buffers. The API allows for reading and writing operations, as well as for subscriptions to individual event streams or all events written.

## EventStoreConnection

The `EventStoreConnection` class maintains a full-duplex connection between the client and the Event Store server. `EventStoreConnection` is thread-safe, and we recommend that you create one instance per application.

Event Store handles all connections asynchronously, returning either a `Task` or a `Task<T>`. If you need to execute synchronously, call `.Wait()` on the asynchronous version.

> [!NOTE]
> To get maximum performance from the connection, we recommend you use it asynchronously.

## Quick start

The code below shows how to connect to an Event Store server, write to a stream, and read back the events. For more detailed information, read the full pages for [connecting to a server](~/dotnet-api/connecting-to-a-server.md), [reading events](~/dotnet-api/reading-events.md) and [writing to a stream](~/dotnet-api/streams.md#writing-to-a-stream)

```csharp
var connection =
    EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

// Don't forget to tell the connection to connect!
connection.ConnectAsync().Wait();

var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
                            Encoding.UTF8.GetBytes("some data"),
                            Encoding.UTF8.GetBytes("some metadata"));

connection.AppendToStreamAsync("test-stream",
                               ExpectedVersion.Any, myEvent).Wait();

var streamEvents =
    connection.ReadStreamEventsForwardAsync("test-stream", 0, 1, false).Result;

var returnedEvent = streamEvents.Events[0].Event;

Console.WriteLine("Read event with data: {0}, metadata: {1}",
    Encoding.UTF8.GetString(returnedEvent.Data),
    Encoding.UTF8.GetString(returnedEvent.Metadata));
```
