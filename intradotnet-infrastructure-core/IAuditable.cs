namespace IntraDotNet.Infrastructure.Core;

/// <summary>
/// Represents an entity that is auditable, meaning it tracks creation, update, and soft delete information.
/// </summary>
/// <remarks>
/// This interface combines the functionalities of ICreateAuditable, IUpdateAuditable, and ISoftDeleteAuditable.
/// It is used to ensure that entities can be audited for creation, last update, and soft deletion.
/// </remarks>
/// <seealso cref="ICreateAuditable"/>
/// <seealso cref="IUpdateAuditable"/>
/// <seealso cref="ISoftDeleteAuditable"/>
public interface IAuditable : ICreateAuditable, IUpdateAuditable, ISoftDeleteAuditable
{
}