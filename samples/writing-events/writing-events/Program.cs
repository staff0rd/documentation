using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace writing_events
{
    class Program
    {
        static async Task Main()
        {
            // Create a connection to the event store.
            var connection = await GetEventStoreConnection("ConnectTo=tcp://admin:changeit@localhost:1113");

            // Generate a collection of events.
            var events = Enumerable
                .Range(1, 10)
                .Select(e => new SimpleEvent {Id = e});

            // Convert them to EventData.
            var eventData = events
                .Select(e => new EventData(
                    eventId: Guid.NewGuid(),
                    type: "simple-event",
                    isJson: true,
                    data: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e)),
                    metadata: null));
            
            // Append the events to the stream.
            await connection.AppendToStreamAsync(
                stream: "write-events", 
                expectedVersion: ExpectedVersion.Any, 
                events: eventData);
        }

        private static async Task<IEventStoreConnection> GetEventStoreConnection(string connectionString)
        {
            var connectionSettings = ConnectionSettings.Create()
                .KeepReconnecting()
                .KeepRetrying();

            var eventStoreConnection =
                EventStoreConnection.Create(connectionString, connectionSettings);

            await eventStoreConnection.ConnectAsync();

            return eventStoreConnection;
        }
    }

    internal class SimpleEvent
    {
        public int Id { get; set; }
    }
}