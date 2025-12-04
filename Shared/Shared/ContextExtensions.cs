using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared;

public static class ContextExtensions {
    private static List<Action<IMutableEntityType>> Conventions = new List<Action<IMutableEntityType>>();

    public static void AddRemovePluralizeConvention(this ModelBuilder builder) {
        Conventions.Add(et => et.SetTableName(et.DisplayName()));
    }

    public static void AddRemoveOneToManyCascadeConvention(this ModelBuilder builder) {
        Conventions.Add(et => et.GetForeignKeys()
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.NoAction));
    }

    public static void ApplyConventions(this ModelBuilder builder) {
        foreach (var entityType in builder.Model.GetEntityTypes()) {
            foreach (Action<IMutableEntityType> action in Conventions)
                action(entityType);
        }

        Conventions.Clear();
    }

    public static void ApplyAuditColumnOrder(this ModelBuilder builder) {
        foreach (var entity in builder.Model.GetEntityTypes()) {
            var entityBuilder = builder.Entity(entity.ClrType);
            var properties = entity.GetProperties().ToList();

            int order = 1; // Start ordering from 1 for non-audit columns

            // Assign order to normal properties first
            foreach (var prop in properties.Where(p => !IsAuditOrTemporalColumn(p.Name))) {
                entityBuilder.Property(prop.Name).HasColumnOrder(order++);
            }

            order = 1000;

            // Now assign order to audit columns (they will be last)
            if (entity.FindProperty("IsActive") != null)
                entityBuilder.Property("IsActive").HasColumnOrder(order++).HasDefaultValue(true);

            if (entity.FindProperty("CreatedBy") != null)
                entityBuilder.Property("CreatedBy").HasColumnOrder(order++);

            if (entity.FindProperty("CreatedAt") != null)
                entityBuilder.Property("CreatedAt").HasColumnOrder(order++);

            if (entity.FindProperty("LastModifiedBy") != null)
                entityBuilder.Property("LastModifiedBy").HasColumnOrder(order++);

            if (entity.FindProperty("LastModifiedAt") != null)
                entityBuilder.Property("LastModifiedAt").HasColumnOrder(order++);

            if (entity.FindProperty("ValidFrom") != null)
                entityBuilder.Property("ValidFrom").HasColumnOrder(order++);

            if (entity.FindProperty("ValidTo") != null)
                entityBuilder.Property("ValidTo").HasColumnOrder(order++);
        }
    }
    public static void ApplyAuditColumnDefaultValue(this ModelBuilder builder) {
        foreach (var entity in builder.Model.GetEntityTypes()) {
            var entityBuilder = builder.Entity(entity.ClrType);

            // Now assign order to audit columns (they will be last)
            if (entity.FindProperty("IsDeleted") != null)
                entityBuilder.Property("IsDeleted").HasDefaultValue(false);

        }
    }
    // Helper method to identify audit/temporal columns
    private static bool IsAuditOrTemporalColumn(string columnName) {
        return columnName switch {
            "IsActive" => true,
            "CreatedBy" => true,
            "CreatedAt" => true,
            "LastModifiedBy" => true,
            "LastModifiedAt" => true,
            "ValidFrom" => true,
            "ValidTo" => true,
            _ => false
        };
    }
}