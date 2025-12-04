using Microsoft.AspNetCore.Identity;
using Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMA.Security.Models;

public class ApplicationUser : IdentityUser<int>, IHasDomainEvent {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public bool IsActive { get; set; } = true;
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new HashSet<ApplicationUserRole>();
}

public class ApplicationRole : IdentityRole<int>, IHasDomainEvent {

    public ApplicationRole() : base() {
    }

    public ApplicationRole(string roleName) : base(roleName) {
    }

    public ApplicationRole(int id, string roleName, string displayName) : base(roleName) {
        Id = id;
        DisplayName = displayName;
        Name = roleName;
    }

    public string? DisplayName { get; set; }
    public bool IsActive { get; set; } = true;
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new HashSet<ApplicationUserRole>();
    public ICollection<ScreenPermission> ScreenPermissions { get; set; } = new HashSet<ScreenPermission>();
}

public class ApplicationUserRole : IdentityUserRole<int> {
    public bool IsActive { get; set; } = true;
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public ApplicationRole Role { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}

public class ApplicationUserPasskey : IdentityUserPasskey<int> {
}

// Custom classes to explicitly map Identity tables
//public class ApplicationUserClaim : IdentityUserClaim<int> { }

//public class ApplicationUserLogin : IdentityUserLogin<int> { }

//public class ApplicationRoleClaim : IdentityRoleClaim<int> { }

//public class ApplicationUserToken : IdentityUserToken<int> { }
