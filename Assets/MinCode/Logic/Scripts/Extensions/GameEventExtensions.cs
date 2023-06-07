using System;
using System.Collections.Generic;

public static class GameEventExtensions
{
    public static void RaiseIfNotNull(this GameEvent gameEvent)
    {
        if (gameEvent != null)
        {
            gameEvent.Raise();
        }
    }

    public static void RaiseIfNotNull(this GameEvent gameEvent, IEnumerable<NamedReferenceSelector> eventParameters)
    {
        if (gameEvent != null)
        {
            gameEvent.Raise(eventParameters);
        }
    }
}
