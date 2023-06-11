
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventstoreBasics.Infrastructure;

public static class EventStoreExtensions
{
  public static Task AppendEvents(this IEventStoreConnection connection,
    string streamName, long version, params object[] events)
  {

      if(events == null || !events.Any()) return Task.CompletedTask;

      var preparedEvents = events.Select(@event =>
        new EventData(Guid.NewGuid(),
        @event.GetType().Name,
        true,
        Serialize(@event),
        Serialize(new EventMetadata
          {ClrType = @event.GetType().AssemblyQualifiedName})
        ))
        .ToArray();

    return connection.AppendToStreamAsync(streamName, version, preparedEvents);
  }

  static byte[] Serialize(object data)
    => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    
}