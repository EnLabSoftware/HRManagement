
namespace Business.Base
{
    public abstract class RootEntity
    {
        private List<BaseDomainEvent> _events;
        public IReadOnlyList<BaseDomainEvent> Events => _events.AsReadOnly();

        protected RootEntity()
        {
            _events = new List<BaseDomainEvent>();
        }

        protected void AddEvent(BaseDomainEvent @event)
        {
            _events.Add(@event);
        }

        protected void RemoveEvent(BaseDomainEvent @event)
        {
            _events.Remove(@event);
        }
    }

    public abstract class BaseEntity<TKey> : RootEntity
    {
        public TKey Id { get; set; }
    }
}