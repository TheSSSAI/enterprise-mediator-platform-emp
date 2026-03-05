using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a high-dimensional vector for AI/ML operations (e.g., semantic search).
    /// Used for matching project requirements with vendor skills.
    /// </summary>
    public class EmbeddingVector : ValueObject
    {
        /// <summary>
        /// The raw vector values.
        /// </summary>
        public ImmutableArray<float> Values { get; }

        /// <summary>
        /// The dimensions of the vector.
        /// </summary>
        public int Dimensions => Values.Length;

        public EmbeddingVector(IEnumerable<float> values)
        {
            if (values is null)
                throw new BusinessRuleValidationException("Vector values cannot be null.");

            Values = values.ToImmutableArray();

            if (Values.IsEmpty)
                throw new BusinessRuleValidationException("Vector cannot be empty.");
        }

        public static EmbeddingVector Create(params float[] values) => new(values);
        public static EmbeddingVector FromList(IEnumerable<float> values) => new(values);

        /// <summary>
        /// Calculates the Cosine Similarity between this vector and another.
        /// Result ranges from -1 (opposite) to 1 (identical).
        /// </summary>
        public double CosineSimilarity(EmbeddingVector other)
        {
            if (other.Dimensions != Dimensions)
                throw new BusinessRuleValidationException($"Vector dimensions mismatch: {Dimensions} vs {other.Dimensions}");

            double dotProduct = 0.0;
            double normA = 0.0;
            double normB = 0.0;

            for (int i = 0; i < Dimensions; i++)
            {
                dotProduct += Values[i] * other.Values[i];
                normA += Math.Pow(Values[i], 2);
                normB += Math.Pow(other.Values[i], 2);
            }

            if (normA == 0 || normB == 0) return 0.0;

            return dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // For vectors, exact float equality can be tricky, but for a ValueObject 
            // identity, we usually require exact match or structural equality.
            // Using the structural equality of the values.
            foreach (var value in Values)
            {
                yield return value;
            }
        }

        public override string ToString()
        {
            return $"Vector({Dimensions})";
        }
    }
}