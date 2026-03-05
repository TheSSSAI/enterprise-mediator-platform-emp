using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMediator.Core.SharedKernel.Common;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public class Result
{
    protected Result(bool isSuccess, string error, IEnumerable<string>? validationErrors = null)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A successful result cannot have an error message.");

        if (!isSuccess && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A failed result must have an error message.");

        IsSuccess = isSuccess;
        Error = error;
        ValidationErrors = validationErrors ?? Enumerable.Empty<string>();
    }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message associated with a failed operation.
    /// </summary>
    public string Error { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets a list of validation errors, if applicable.
    /// </summary>
    public IEnumerable<string> ValidationErrors { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful result.</returns>
    public static Result Success() => new(true, string.Empty);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A failed result.</returns>
    public static Result Failure(string error) => new(false, error);

    /// <summary>
    /// Creates a failed result with validation errors.
    /// </summary>
    /// <param name="error">The error summary.</param>
    /// <param name="validationErrors">The collection of validation errors.</param>
    /// <returns>A failed result.</returns>
    public static Result Failure(string error, IEnumerable<string> validationErrors) => new(false, error, validationErrors);
}

/// <summary>
/// Represents the result of an operation that returns a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class Result<T> : Result
{
    private readonly T? _value;

    protected Result(T? value, bool isSuccess, string error, IEnumerable<string>? validationErrors = null)
        : base(isSuccess, error, validationErrors)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value returned by the operation. Throws if accessed on a failed result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if accessed when IsSuccess is false.</exception>
    public T Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException("The value of a failure result can not be accessed.");

            return _value!;
        }
    }

    /// <summary>
    /// Gets the value if successful, or default otherwise.
    /// </summary>
    public T? ValueOrDefault => _value;

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns>A successful result.</returns>
    public static Result<T> Success(T value) => new(value, true, string.Empty);

    /// <summary>
    /// Creates a failed result for a generic type.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A failed result.</returns>
    public new static Result<T> Failure(string error) => new(default, false, error);

    /// <summary>
    /// Creates a failed result with validation errors for a generic type.
    /// </summary>
    /// <param name="error">The error summary.</param>
    /// <param name="validationErrors">The collection of validation errors.</param>
    /// <returns>A failed result.</returns>
    public new static Result<T> Failure(string error, IEnumerable<string> validationErrors) => new(default, false, error, validationErrors);
    
    public static implicit operator Result<T>(T value) => Success(value);
}