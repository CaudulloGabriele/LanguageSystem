using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Static class containing useful methods to work with the LanguageSystem
/// </summary>
public static class LanguageSystemExtensions
{

    /// <summary>
    /// Returns the only '<see cref="LanguageValue{TValue}"/>' in the enumeration with the desired value
    /// </summary>
    /// <typeparam name="TValue">Type of the value of the '<see cref="LanguageValue{TValue}"/>'</typeparam>
    /// <param name="enumerable">Enumeration containing the values for the different languages in the game</param>
    /// <param name="value">Value to use to search for the desired '<see cref="LanguageValue{TValue}"/>'</param>
    /// <returns></returns>
    public static LanguageValue<TValue> GetSingleWithValue<TValue>(this IEnumerable<LanguageValue<TValue>> enumerable, TValue value)
        => enumerable.Single(lv => (lv.Value.ToString() == value.ToString()));

}