using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public interface ICurrentUserService {
    string? UserId { get; }
    string? UserName { get; }
    string? UserRole { get; }
}
