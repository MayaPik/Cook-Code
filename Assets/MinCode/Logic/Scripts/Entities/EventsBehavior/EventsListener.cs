using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Listens to multiple events, and executes actions once any of the events were raised
/// </summary>
public class EventsListener : ActionsExecutor
{
    public List<GameEvent> events;
    private List<EventListener> eventListeners;

    public override void SetExecuteActions(bool newValue)
    {
        foreach (var listener in eventListeners.Where(l => l != null))
        {
            listener.SetExecuteActions(newValue);
        }
    }

    public override void ExecuteActions(string debugInfo)
    {
        foreach (var listener in eventListeners.Where(l => l != null))
        {
            listener.ExecuteActions(debugInfo);
        }
    }

    public override void ExecuteActions(UnityEvent actions)
    {
        foreach (var listener in eventListeners.Where(l => l != null))
        {
            listener.ExecuteActions(actions);
        }
    }

    public void OnEnable()
    {
        if (eventListeners == null)
        {
            eventListeners = new List<EventListener>();

            foreach (var currentEvent in events)
            {
                var eventListener = gameObject.AddComponent<EventListener>();
                eventListener.Event = currentEvent;
                eventListener.actions = actions;
                eventListener.executeActions = executeActions;
                eventListener.printDebugInfo = printDebugInfo;
                eventListener.Set();

                eventListeners.Add(eventListener);
            }
        }
        else
        {
            foreach (var listener in eventListeners.Where(l => l != null))
            {
                listener.OnEnable();
            }
        }
    }

    private void OnDestroy()
    {
        if (eventListeners != null)
        {
            foreach (var listener in eventListeners.Where(l => l != null))
            {
                Destroy(listener);
            }
        }
    }

    public void OnDisable()
    {
        if (eventListeners != null)
        {
            foreach (var listener in eventListeners.Where(l => l != null))
            {
                listener.OnDisable();
            }
        }
    }
}
