using System.Collections.Generic;

namespace Domain.Base
{
    public abstract class BaseEntity
    {
        public virtual List<BaseDomainEvent> Events { get; init; } = new List<BaseDomainEvent>();
    }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; set; }
    }
}