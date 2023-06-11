namespace EventstoreBasics.Infrastructure;

public abstract class Entity
{
  protected void Apply(object @event)
  {
    _changes.Add(@event);

    When(@event);
  }

  protected abstract void When(object @event);

  public IReadOnlyCollection<Object> Changes => _changes.AsReadOnly();
  List<Object> _changes = new List<object>();

  public abstract string GetId();

  public void ClearChanges() => _changes.Clear();

  public int Version {get; set;} = -1;
}