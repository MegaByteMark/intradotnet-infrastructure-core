# IntraDotNet.Domain.Core

The `IntraDotNet.Domain.Core` library provides a set of interfaces that help implement common auditing and versioning patterns in your domain entities. These interfaces are designed to work seamlessly with Entity Framework Core and other ORM frameworks to automatically track entity lifecycle events.

## Features

- **Audit Tracking**: Automatically track creation, update, and soft deletion events
- **Concurrency Control**: Built-in row versioning support for optimistic concurrency
- **Flexible Implementation**: Modular interfaces that can be implemented individually or combined
- **.NET 9.0 Support**: Built for the latest .NET framework with nullable reference types enabled

## Interfaces

### Core Auditing Interfaces

#### [`ICreateAuditable`](intradotnet-domain-core/ICreateAuditable.cs)
Tracks entity creation information:
```csharp
public interface ICreateAuditable
{
    public DateTimeOffset? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
}
```

#### [`IUpdateAuditable`](intradotnet-domain-core/IUpdateAuditable.cs)
Tracks entity update information:
```csharp
public interface IUpdateAuditable
{
    public DateTimeOffset? LastUpdateOn { get; set; }
    public string? LastUpdateBy { get; set; }
}
```

#### [`ISoftDeleteAuditable`](intradotnet-domain-core/ISoftDeleteAuditable.cs)
Tracks soft deletion without physically removing entities:
```csharp
public interface ISoftDeleteAuditable
{
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
}
```

#### [`IAuditable`](intradotnet-domain-core/IAuditable.cs)
Combines all auditing interfaces for comprehensive tracking:
```csharp
public interface IAuditable : ICreateAuditable, IUpdateAuditable, ISoftDeleteAuditable
{
}
```

### Concurrency Control

#### [`IRowVersion`](intradotnet-domain-core/IRowVersion.cs)
Provides optimistic concurrency control:
```csharp
public interface IRowVersion
{
    [Timestamp]
    byte[]? RowVersion { get; set; }
}
```

## Usage Examples

### Basic Entity with Full Auditing

```csharp
using IntraDotNet.Domain.Core;

public class Product : IAuditable, IRowVersion
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    // IAuditable properties
    public DateTimeOffset? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastUpdateOn { get; set; }
    public string? LastUpdateBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    
    // IRowVersion property
    public byte[]? RowVersion { get; set; }
}
```

### Entity with Selective Auditing

```csharp
using IntraDotNet.Domain.Core;

public class Category : ICreateAuditable, IUpdateAuditable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Only track creation and updates, not deletions
    public DateTimeOffset? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastUpdateOn { get; set; }
    public string? LastUpdateBy { get; set; }
}
```

### Entity Framework Core Integration

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    public override int SaveChanges()
    {
        UpdateAuditableEntities();
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateAuditableEntities()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            
        var currentUser = GetCurrentUser(); // Implement your user resolution logic
        var timestamp = DateTimeOffset.UtcNow;
        
        foreach (var entry in entries)
        {
            if (entry.Entity is ICreateAuditable createAuditable && entry.State == EntityState.Added)
            {
                createAuditable.CreatedOn = timestamp;
                createAuditable.CreatedBy = currentUser;
            }
            
            if (entry.Entity is IUpdateAuditable updateAuditable && entry.State == EntityState.Modified)
            {
                updateAuditable.LastUpdateOn = timestamp;
                updateAuditable.LastUpdateBy = currentUser;
            }
        }
    }
    
    private string GetCurrentUser()
    {
        // Implement your logic to get the current user
        return "system"; // placeholder
    }
}
```

### Soft Delete Implementation

```csharp
public static class SoftDeleteExtensions
{
    public static void SoftDelete<T>(this T entity, string deletedBy) 
        where T : ISoftDeleteAuditable
    {
        entity.DeletedOn = DateTimeOffset.UtcNow;
        entity.DeletedBy = deletedBy;
    }
    
    public static IQueryable<T> WhereNotDeleted<T>(this IQueryable<T> query) 
        where T : ISoftDeleteAuditable
    {
        return query.Where(e => e.DeletedOn == null);
    }
}

// Usage
var product = await context.Products.FindAsync(id);
if (product != null)
{
    product.SoftDelete("current-user");
    await context.SaveChangesAsync();
}

// Query non-deleted entities
var activeProducts = context.Products.WhereNotDeleted().ToList();
```

## Installation

Install the package via NuGet:

```bash
dotnet add package IntraDotNet.Domain.Core
```

Or via Package Manager Console:

```powershell
Install-Package IntraDotNet.Domain.Core
```

## Requirements

- .NET 9.0 or later
- Compatible with Entity Framework Core and other ORM frameworks

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Repository

[https://github.com/MegaByteMark/intradotnet-domain-core](https://github.com/MegaByteMark/intradotnet-domain-core)