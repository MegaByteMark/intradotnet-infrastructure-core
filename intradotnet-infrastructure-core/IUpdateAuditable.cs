namespace IntraDotNet.Infrastructure.Core;

/// <summary>
/// Represents an entity that is auditable, meaning it tracks update information.
/// </summary>
/// <remarks>
/// This interface is used to ensure that entities can be audited for update details.
/// It is typically used in conjunction with other auditing interfaces like ICreateAuditable and ISoftDeleteAuditable.
/// </remarks>
/// <seealso cref="ICreateAuditable"/>
/// <seealso cref="ISoftDeleteAuditable"/>
/// <seealso cref="IAuditable"/>
public interface IUpdateAuditable
{
    public DateTimeOffset? LastUpdateOn { get; set; }
    public string? LastUpdateBy { get; set; }
}