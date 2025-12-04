using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public interface IDomainEventService {
    Task Publish(DomainEvent domainEvent);
}
