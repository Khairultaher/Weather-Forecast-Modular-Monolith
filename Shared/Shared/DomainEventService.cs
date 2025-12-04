using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared;

public interface IHasDomainEvent {
    public List<DomainEvent> DomainEvents { get; set; }
}

public abstract class HasDomainEvent : IHasDomainEvent {
    [NotMapped]
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
public abstract class DomainEvent {
    protected DomainEvent() {
        DateOccurred = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}


