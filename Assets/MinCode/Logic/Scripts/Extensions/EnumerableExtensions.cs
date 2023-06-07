using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    /// <summary>
    /// Prints an IEnumerable
    /// </summary>
    /// <param name="toPrint">The IEnumerable to print</param>
    /// <param name="getStringFunc">A customized string resolve method</param>
    /// <returns>A string of that list, separated with commas</returns>
    public static string Print<T>(this IEnumerable<T> toPrint, Func<T, string> getStringFunc = null)
    {
        const string Separator = ", ";
        var result = "null";

        if (toPrint != null)
        {
            if (getStringFunc != null)
            {
                result = string.Join(Separator, toPrint.Select(t => getStringFunc.Invoke(t)));
            }
            else
            {
                result = string.Join(Separator, toPrint);
            }
        }

        return $"'{result}'";
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable == null || !enumerable.Any();
    }

    /// <summary>
    /// Wraps an object in an IEnumerable
    /// </summary>
    /// <param name="obj">Object to wrap</param>
    /// <returns>An IEnumerable that wraps the object</returns>
    public static IEnumerable<T> YieldSingle<T>(this T obj)
    {
        yield return obj;
    }

    /// <summary>
    /// Wraps an object in an IEnumerable, and appends a variable to it
    /// </summary>
    /// <param name="obj">Object to wrap</param>
    /// <param name="toAppend">Object to append</param>
    /// <returns>An IEnumerable that wraps the object</returns>
    public static IEnumerable<T> YieldAppend<T>(this T obj, T toAppend)
    {
        yield return obj;
        yield return toAppend;
    }
}
