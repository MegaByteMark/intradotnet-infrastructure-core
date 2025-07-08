namespace IntraDotNet.Infrastructure.Core;

/// <summary>
/// Represents an entity that is soft-deletable, meaning it tracks deletion information without physically removing it from the database.
/// </summary>
/// <remarks>
/// This interface is used to ensure that entities can be marked as deleted without being physically removed.
/// It is typically used in conjunction with other auditing interfaces like ICreateAuditable and IUpdateAuditable.
/// </remarks>
/// <seealso cref="ICreateAuditable"/>
/// <seealso cref="IUpdateAuditable"/>
/// <seealso cref="IAuditable"/>
public interface ISoftDeleteAuditable
{
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }

}