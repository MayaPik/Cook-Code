using System;
public static class FloatExtensions
{
    public static string GetString(this float val)
    {
        return val.ToString("F5");
    }
}
