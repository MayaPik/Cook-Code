using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Game event that can be listened and raised
/// </summary>
[CreateAssetMenu(menuName = "Custom Variables/Event")]
public class GameEvent : ScriptableObject
{
    [HideInInspector]
    public IReadOnlyList<NamedReferenceSelector> eventParameters;

    [HideInInspector]
    public List<EventListener> Listeners;
    public string ListenersNames(bool separateLines = false) => Listeners == null ? "No listeners!" : string.Join(separateLines ? $", {Environment.NewLine}" : ", ", Listeners.Select(l => l?.name));

    public bool printDebugInfo;
    public UnityEvent afterRaise;

    private void OnEnable()
    {
        Listeners = new List<EventListener>();
    }

    public void SetParameters(IReadOnlyList<NamedReferenceSelector> parameters)
    {
        eventParameters = parameters;
    }

    public void Raise()
    {
        Raise(eventParameters ?? Enumerable.Empty<NamedReferenceSelector>());
    }

    public void RaiseWithParameter(string name, object value)
    {
        Raise(new Dictionary<string, object> { { name, value } });
    }

    public void Raise(IEnumerable<(string name, object value)> eventParameters)
    {
        var parametersAsDictionary = eventParameters
            .GroupBy(p => p.name)
            .ToDictionary(p => p.Key, p => p.First().value);

        Raise(parametersAsDictionary);
    }

    public void Raise(IEnumerable<NamedReferenceSelector> eventParameters)
    {
        Raise(GetParametersByName(eventParameters));
    }

    public void Raise(IDictionary<string, object> eventParameters)
    {
        if (printDebugInfo)
        {
            var parametersString =
                eventParameters.IsNullOrEmpty()
                ? "no parameters"
                : string.Join(", ", eventParameters.Take(10).Select(param => $"{param.Key}: {param.Value}"));

            Debug.Log($"Raising event '{name}' for: {Listeners?.Count ?? 0} listeners! {ListenersNames()}. Parameters: {parametersString}");
        }

        if (Listeners != null)
        {
            Listeners = Listeners.Where(l => l != null).ToList();

            foreach (var listener in Listeners.ToList())
            {
                listener.OnEventRaised(eventParameters);
            }
        }

        afterRaise.Invoke();
    }

    public void AddListener(EventListener eventListener)
    {
        if (!Listeners.Contains(eventListener) && eventListener != null)
        {
            Listeners.Add(eventListener);
        }
    }

    public void RemoveListener(EventListener eventListener)
    {
        if (Listeners.Contains(eventListener))
        {
            Listeners.Remove(eventListener);
        }
    }

    public IDictionary<string, object> GetParametersByName(IEnumerable<NamedReferenceSelector> eventParameters)
    {
        var nonNullParameters = eventParameters ?? Enumerable.Empty<NamedReferenceSelector>();

        return nonNullParameters
            .GroupBy(p => p.name)
            .ToDictionary(k => k.Key, v => v.First().value.GetReferenceValue(printDebugInfo));
    }
}
