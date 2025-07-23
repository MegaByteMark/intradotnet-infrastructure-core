using System.ComponentModel.DataAnnotations;

namespace IntraDotNet.Domain.Core;

/// <summary>
/// Represents an entity that has a row version for concurrency control.
/// </summary>
/// <remarks>
/// This interface is used to ensure that entities can be versioned for concurrency control.
/// It is typically used in conjunction with other auditing interfaces like ICreateAuditable and IUpdateAuditable.
/// </remarks>
/// <seealso cref="ICreateAuditable"/>
/// <seealso cref="IUpdateAuditable"/>
/// <seealso cref="IAuditable"/>
/// <seealso cref="ISoftDeleteAuditable"/>
public interface IRowVersion
{
    [Timestamp]
    byte[]? RowVersion { get; set; }
}