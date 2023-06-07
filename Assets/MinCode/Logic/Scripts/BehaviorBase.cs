using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The base class for most of the scripts
/// Lets you turn on and off debug prints easily, and wraps the debug messages in valuable info
/// </summary>
public class BehaviorBase : MonoBehaviour
{
    public BooleanReference printDebugInfo;
    protected Func<string> GetNameDelegate { get; set; }
    protected Func<string> CustomDebugPrefix { get; set; }

    protected void PrintError(string message)
    {
        Debug.LogError($"{DebugPrefix}{message}");
    }

    protected void PrintDebugInfoIgnoreConfig(string message)
    {
        print($"{DebugPrefix}{message}");
    }

    protected void PrintDebugInfo(string message)
    {
        if (printDebugInfo)
        {
            PrintDebugInfoIgnoreConfig(message);
        }
    }

    protected string DebugPrefix
    {
        get
        {
            var debugPrefix = CustomDebugPrefix != null ? $" [{CustomDebugPrefix.Invoke()}]" : string.Empty;
            var myName = GetNameDelegate != null ? GetNameDelegate.Invoke() : name;

            return $"'{myName}' " +
            $"at {transform.position.GetString()}" +
            $"{debugPrefix}: ";
        }
    }
}
