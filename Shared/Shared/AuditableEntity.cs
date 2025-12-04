using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public abstract class AuditableEntity  {

    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
