using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[Serializable]
public class KeyedAction
{
    public StringReference key;
    public UnityEvent action;
    public BooleanReference execute = BooleanReference.Create(true);
}

/// <summary>
/// The base class for classes that executes actions
/// You can also work with additionalActions to execute specific actions sets by name (without the need to code it menually! woohoo)
/// </summary>
public class ActionsExecutor : BehaviorBase
{
    public BooleanReference executeActions = BooleanReference.Create<BooleanReference>(true);
    public UnityEvent actions;
    public List<KeyedAction> additionalActions;
    private Dictionary<string, KeyedAction> keyedActions;

    protected void Awake()
    {
        keyedActions = additionalActions?.ToDictionary(k => k.key.Value, v => v)
            ?? new Dictionary<string, KeyedAction>();
    }

    public void Set(ActionsExecutor actionsExecutor)
    {
        actions = actionsExecutor.actions;
        executeActions = actionsExecutor.executeActions;
        printDebugInfo = actionsExecutor.printDebugInfo;
    }

    public virtual void SetExecuteActions(bool newValue)
    {
        PrintDebugInfo($"Setting 'executeActions' from {executeActions} to {newValue}. Reference: {executeActions.variable?.name}");
        executeActions.SetValue(newValue);
    }

    public virtual void ExecuteAdditionalActions(string key)
    {
        PrintDebugInfo($"ExecuteAdditionalActions called. executeActions: {executeActions}, key: {key}, keyedActions.Keys: {keyedActions?.Keys.Print()}");
        if (key != null && keyedActions.TryGetValue(key, out var keyedAction) && keyedAction?.action != null && keyedAction.execute)
        {
            keyedAction.action.Invoke();
        }
        else
        {
            Debug.LogWarning($"{DebugPrefix} a try to invoke invalid action key was made: {key}");
        }
    }

    public virtual void ExecuteActions(UnityEvent actions)
    {
        PrintDebugInfo($"ExecuteActions called. executeActions: {executeActions}");
        if (executeActions && actions != null)
        {
            actions.Invoke();
        }
    }

    public virtual void ExecuteActions(string debugLog = null)
    {
        if (debugLog != null)
        {
            PrintDebugInfo(debugLog);
        }

        ExecuteActions(actions);
    }
}
