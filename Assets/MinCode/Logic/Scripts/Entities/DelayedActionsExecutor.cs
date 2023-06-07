using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DelayedAction
{
    public FloatReference delaySeconds;
    public UnityEvent actions;
    public BooleanReference playOnAwake = BooleanReference.Create<BooleanReference>(false);
}

/// <summary>
/// Executes actions that should be delayed
/// </summary>
public class DelayedActionsExecutor : BehaviorBase
{
    public List<BooleanReference> executeActions = BooleanReference.Create(true).YieldSingle().ToList();
    public List<DelayedAction> delayedActions;
    private Coroutine lastCoroutine;

    public void WaitAndAction(int index)
    {
        PrintDebugInfo($"WaitAndAction({index}) command from '{name}'. executeActions: {executeActions.All(b => b)}");

        if (executeActions.All(b => b))
        {
            if (delayedActions.Count >= index && index < delayedActions.Count)
            {
                lastCoroutine = StartCoroutine(WaitAndExecute(delayedActions[index]));
            }
            else if (printDebugInfo)
            {
                PrintDebugInfo($"WaitAndAction({index}) couldn't find action at index {index} ('{name}')");
            }
        }
    }

    public void CancelExecution()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaitAndExecute(DelayedAction action)
    {
        PrintDebugInfo($"'{name}' executing delayed action command. action.delaySeconds: {action.delaySeconds}");

        if (lastCoroutine != null)
        {
            StopCoroutine(lastCoroutine);
        }

        yield return new WaitForSecondsRealtime(action.delaySeconds);
        action.actions.Invoke();

        PrintDebugInfo($"'{name}' done executing delayed actions");
    }

    private void OnEnable()
    {
        foreach (var delayedAction in delayedActions)
        {
            if (delayedAction.playOnAwake)
            {
                WaitAndExecute(delayedAction);
            }
        }
    }
}
