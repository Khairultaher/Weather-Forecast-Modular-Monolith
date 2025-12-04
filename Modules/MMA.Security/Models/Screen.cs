using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Security.Models;

public class Screen : AuditableEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ScreenPermission> ScreenPermissions { get; set; } = new HashSet<ScreenPermission>();
}
