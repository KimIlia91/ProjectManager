using System.ComponentModel;

namespace PM.Domain.Common.Extensions;

/// <summary>
/// Provides extension methods for working with enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description attribute value of an enum value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="enumerationValue">The enum value.</param>
    /// <returns>The description attribute value if available; otherwise, the enum value's name.</returns>
    /// <exception cref="ArgumentException">Thrown if the provided value is not an enum.</exception>
    public static string GetDescription<TEnum>(this TEnum enumerationValue) where TEnum : Enum
    {
        var type = enumerationValue.GetType();
        if (!type.IsEnum) throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type");

        var memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return enumerationValue.ToString();
    }

    /// <summary>
    /// Gets the description attribute value of an enum value or returns null if the value is null.
    /// </summary>
    /// <param name="enumValue">The enum value.</param>
    /// <returns>The description attribute value if available, or null if the value is null.</returns>
    public static string? GetDescriptionOrNull(this Enum enumValue)
    {
        return enumValue == null ? null : GetDescription(enumValue);
    }

    /// <summary>
    /// Retrieves all enum values of a specific type as an enumerable.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>An enumerable of all enum values.</returns>
    public static IEnumerable<TEnum> GetAllAsEnumerable<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
    }

    /// <summary>
    /// Converts enum values into a collection of EnumResult objects.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>A collection of EnumResult objects containing enum information.</returns>
    public static IEnumerable<EnumResult> ToEnumResults<TEnum>() where TEnum : Enum
    {
        return ToEnumResults(GetAllAsEnumerable<TEnum>().ToArray());
    }

    /// <summary>
    /// Converts specific enum values into a collection of EnumResult objects.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="values">The enum values to convert.</param>
    /// <returns>A collection of EnumResult objects containing enum information.</returns>
    public static IEnumerable<EnumResult> ToEnumResults<TEnum>(params TEnum[] values) where TEnum : Enum
    {
        return values.ToList()
            .Select(t => new EnumResult
            {
                Id = (int)(object)t,
                Name = t.ToString()
            });
    }
}
