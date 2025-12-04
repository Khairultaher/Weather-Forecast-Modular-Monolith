using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Security.Models;

public class ScreenPermission : AuditableEntity {
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int ScreenId { get; set; }
    public bool IsActive { get; set; }
    public Screen Screen { get; set; } = null!;
    public ApplicationRole Role { get; set; } = null!;
}
