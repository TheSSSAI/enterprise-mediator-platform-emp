using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.VendorManagement.Aggregates;

/// <summary>
/// Represents a specific skill or area of expertise possessed by a vendor.
/// Can optionally include a vector embedding for semantic matching.
/// </summary>
public class VendorSkill : Entity<Guid>
{
    public VendorId VendorId { get; private set; }
    public string Name { get; private set; }
    public EmbeddingVector? Embedding { get; private set; }
    public DateTime AddedAt { get; private set; }

    // EF Core constructor
    protected VendorSkill() { }

    private VendorSkill(Guid id, VendorId vendorId, string name, EmbeddingVector? embedding)
    {
        Id = id;
        VendorId = vendorId;
        Name = name;
        Embedding = embedding;
        AddedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Creates a new Vendor Skill.
    /// </summary>
    public static VendorSkill Create(VendorId vendorId, string name, EmbeddingVector? embedding = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Skill name cannot be empty.");

        return new VendorSkill(Guid.NewGuid(), vendorId, name.Trim(), embedding);
    }

    /// <summary>
    /// Updates the vector embedding for this skill, typically after an async AI process.
    /// </summary>
    /// <param name="embedding">The calculated embedding vector.</param>
    public void SetEmbedding(EmbeddingVector embedding)
    {
        Embedding = embedding ?? throw new ArgumentNullException(nameof(embedding));
    }

    /// <summary>
    /// Renames the skill. 
    /// </summary>
    /// <param name="newName">The new name for the skill.</param>
    /// <remarks>This might invalidate the embedding if the semantic meaning changes.</remarks>
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new BusinessRuleValidationException("Skill name cannot be empty.");

        if (Name.Equals(newName.Trim(), StringComparison.OrdinalIgnoreCase))
            return;

        Name = newName.Trim();
        // Domain decision: Does renaming invalidate the embedding? 
        // Assuming yes, as "Java" vs "JavaScript" are semantically different.
        Embedding = null; 
    }
}