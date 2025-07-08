namespace IntraDotNet.Infrastructure.Core;

/// <summary>
/// Represents an entity that is auditable, meaning it tracks creation information.
/// </summary>
/// <remarks>
/// This interface is used to ensure that entities can be audited for creation details.
/// It is typically used in conjunction with other auditing interfaces like IUpdateAuditable and ISoftDeleteAuditable.
/// </remarks>
/// <seealso cref="IUpdateAuditable"/>
/// <seealso cref="ISoftDeleteAuditable"/>
/// <seealso cref="IAuditable"/>
public interface ICreateAuditable
{
    public DateTimeOffset? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
}