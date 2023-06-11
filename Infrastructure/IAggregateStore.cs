namespace EventstoreBasics.Infrastructure;

public interface IAggregateStore
{
  Task Store<T>(T entity) where T : Entity;

  Task<T> Load<T>(string id)  where T : Entity;
}