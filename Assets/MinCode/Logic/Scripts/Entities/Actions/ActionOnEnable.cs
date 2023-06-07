using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ActionOnEnable : ActionsExecutor
{
    private void OnEnable()
    {
        PrintDebugInfo("enabling");
        ExecuteActions();
    }
}
