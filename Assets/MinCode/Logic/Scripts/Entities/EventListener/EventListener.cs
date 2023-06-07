using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Listens to event and execute actions when the event is raised
/// If event has parameters, you can get it using GetEventParameter
/// </summary>
public class EventListener : ActionsExecutor
{
    public delegate void EventRaisedDelegate(IDictionary<string, object> parametersByName);
    [Tooltip("The event to listen to")]
    public GameEvent Event;
    [HideInInspector]
    public event EventRaisedDelegate EventRaisedEvent;

    public void Set(GameEvent gameEvent, ActionsExecutor executorParameters)
    {
        Event = gameEvent;
        Set(executorParameters);

        Set();
    }

    public virtual void OnEventRaised(IDictionary<string, object> parameters)
    {
        EventRaisedEvent?.Invoke(parameters);
        ExecuteActions();
    }

    public TParameter GetEventParameter<TParameter>(string parameterName, IDictionary<string, object> parametersByName)
    {
        if (parametersByName.TryGetValue(parameterName, out var parameter))
        {
            if (parameter is TParameter parameterOfTypeT)
            {
                return parameterOfTypeT;
            }
            else
            {
                Debug.LogError($"Invalid parameter type: '{typeof(TParameter).Name}'. Correct type is {parameter.GetType().Name}. Parameter name: {parameterName}");
            }
        }
        else
        {
            Debug.LogError($"Invalid event parameter name: '{parameterName}', parameter is not a part of the raised '{Event.name}' event");
        }

        return default;
    }

    public void Set()
    {
        if (Event != null)
        {
            Event.AddListener(this);
        }
    }

    public virtual void OnEnable()
    {
        Set();
    }

    private void OnDestroy()
    {
        if (Event != null)
        {
            Event.RemoveListener(this);
        }
    }

    public void OnDisable()
    {
        if (Event != null)
        {
            Event.RemoveListener(this);
        }
    }
}
