using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ActionOnDisable : ActionsExecutor
{
    private void OnDisable()
    {
        PrintDebugInfo("disabling");
        ExecuteActions();
    }
}
