using EventStore.ClientAPI;

namespace EventstoreBasics.Infrastructure;

public class AggregateStore : IAggregateStore
{
  readonly IEventStoreConnection _connection;

  public AggregateStore(IEventStoreConnection connection)
  {
    _connection = connection;
  }

  public Task<T> Load<T>(string id) where T : Entity
  {
    var entity = (T)Activator.CreateInstance(typeof(T), true); // fake code
    return Task.FromResult(entity);
  }

  public async Task Store<T>(T entity) where T : Entity
  {
    if(entity == null){
      throw new ArgumentNullException(nameof(entity));
    }
    
    var streamName = GetStreamName<T>(entity.GetId());

    var changes = entity.Changes.ToArray();

    await _connection.AppendEvents(streamName, entity.Version, changes);

    entity.ClearChanges();
  }

  static string GetStreamName<T>(string entityId)
    => $"{typeof(T).Name}-{entityId}";
}