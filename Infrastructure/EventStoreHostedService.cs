using EventStore.ClientAPI;

namespace EventstoreBasics.Infrastructure;

public class EventStoreHostedService : IHostedService
{
  public EventStoreHostedService(IEventStoreConnection connection)
  {
    _connection = connection;
  }

  readonly IEventStoreConnection _connection ;
  EventStoreSubscription _subscription;

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await _connection.ConnectAsync();

    // _subscription = new EventStoreSubscription(_connection);
    // _subscription.Start();

  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    _connection.Close();
    return Task.CompletedTask;
  }
}