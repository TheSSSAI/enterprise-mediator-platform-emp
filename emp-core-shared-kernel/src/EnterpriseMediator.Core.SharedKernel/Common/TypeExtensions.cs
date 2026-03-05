using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMediator.Core.SharedKernel.Common;

/// <summary>
/// Provides extension methods for Type and generic operations.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Checks if a type implements a specific generic interface.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="interfaceType">The generic interface type definition (e.g. typeof(IRepository&lt;&gt;)).</param>
    /// <returns>True if the type implements the generic interface; otherwise, false.</returns>
    public static bool ImplementsGenericInterface(this Type type, Type interfaceType)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

        return type.GetInterfaces().Any(i => 
            i.IsGenericType && 
            i.GetGenericTypeDefinition() == interfaceType);
    }

    /// <summary>
    /// Gets the friendly name of a type (e.g., "List&lt;string&gt;" instead of "List`1").
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The friendly name string.</returns>
    public static string GetFriendlyName(this Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        if (!type.IsGenericType)
            return type.Name;

        var typeName = type.Name.Substring(0, type.Name.IndexOf('`'));
        var genericArgs = type.GetGenericArguments();
        
        return $"{typeName}<{string.Join(",", genericArgs.Select(GetFriendlyName))}>";
    }

    /// <summary>
    /// Determines whether the object is null or its default value.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if default/null; otherwise false.</returns>
    public static bool IsDefault<T>(this T value)
    {
        return EqualityComparer<T>.Default.Equals(value, default);
    }
}