using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Toggles a boolean variable, and executes actions according to its values
/// </summary>
public class BoolToggler : BehaviorBase
{
    public bool refreshOnEnabled = true;
    public UnityEvent onTrue;
    public UnityEvent onFalse;
    [Header("The boolean to read and change its values")]
    public BooleanReference toToggle;

    private void OnEnable()
    {
        if (refreshOnEnabled)
        {
            Refresh();
        }

        toToggle.CreateChangeEventListenerIfThereIs(gameObject, BooleanReference.Create(false), Refresh);
    }

    public void Toggle()
    {
        PrintDebugInfo($"toggling value from {toToggle} to {!toToggle}");
        toToggle.SetValue(!toToggle);
        Refresh();
    }

    public void Refresh()
    {
        PrintDebugInfo($"refreshing. Executing {toToggle} actions");

        if (toToggle)
        {
            onTrue.Invoke();
        }
        else
        {
            onFalse.Invoke();
        }
    }
}
