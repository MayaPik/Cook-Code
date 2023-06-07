using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Raises a game event
/// </summary>
public class EventRaiser : BehaviorBase
{
    public List<NamedReferenceSelector> eventParameters;
    public GameEvent gameEvent;

    public void RaiseEvent()
    {
        PrintDebugInfo($"raising event {gameEvent?.name}");
        gameEvent.SetParameters(eventParameters);
        gameEvent.RaiseIfNotNull();
    }
}
