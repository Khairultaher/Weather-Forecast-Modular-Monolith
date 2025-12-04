using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Security.Models;

public partial class AuthRefreshToken : AuditableEntity {
    public Guid Id { get; set; } = Guid.NewGuid();
    public long UserId { get; set; }
    public int RoleId { get; set; }
    public string? UserName { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime ExpireDate { get; set; }
}
