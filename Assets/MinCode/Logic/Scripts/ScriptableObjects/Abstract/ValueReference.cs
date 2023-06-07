using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The generic implementation of ValueReference - feel free to inherit from it to your use!
/// Can be set to a constant or a variable
/// </summary>
/// <typeparam name="TConst">The underlying value's type</typeparam>
/// <typeparam name="TVar">Variable type</typeparam>
public abstract class ValueReference<TConst, TVar> where TVar : VariableScriptableObject<TConst>
{
    [HideInInspector]
    public TConst constant;
    [HideInInspector]
    public TVar variable;
    [HideInInspector]
    public ReferenceOption selectedReference;

    public bool HasValue => Value != null;

    public bool HasChangeEvent => variable?.EventOnChange != null;

    public string Name => variable?.name ?? $"constant {typeof(TConst).Name}";

    public TConst Value
    {
        get
        {
            return GetValue();
        }
        set
        {
            SetValue(value);
        }
    }

    public EventListener CreateChangeEventListenerIfThereIs(GameObject listener, BooleanReference printDebugInfo, UnityAction action)
    {
        return HasChangeEvent
            ? CreateChangeEventListener(listener, printDebugInfo, action)
            : null;
    }

    public EventListener CreateChangeEventListener(GameObject listener, BooleanReference printDebugInfo, UnityAction action)
    {
        EventListener changeListener = null;

        if (printDebugInfo)
        {
            Debug.Log($"adding listener to {listener.name}. variable: {variable?.name ?? "null"}, variable?.EventOnChange: {variable?.EventOnChange.name ?? "null"}");
        }

        if (HasChangeEvent)
        {
            changeListener = listener.AddComponent<EventListener>();
            changeListener.Event = variable.EventOnChange;
            changeListener.actions = new UnityEvent();
            changeListener.actions.AddListener(action);
            changeListener.executeActions = BooleanReference.Create<BooleanReference>(true);
            changeListener.printDebugInfo = printDebugInfo;
            changeListener.Set();
        }
        else
        {
            Debug.LogError($"can't add listener to {listener.name}. variable: {variable?.name ?? "null"}, it doesn't have event on change reference");
        }

        return changeListener;
    }

    public override string ToString()
    {
        return Value?.ToString();
    }

    public void SetValue(TConst newValue)
    {
        switch (selectedReference)
        {
            case ReferenceOption.Constant:
                constant = newValue;
                break;
            case ReferenceOption.Variable:
                variable.SetValue(newValue);
                break;
            default:
                throw new ArgumentOutOfRangeException(selectedReference.ToString() + " is not supported");
        }
    }

    private TConst GetValue()
    {
        switch (selectedReference)
        {
            case ReferenceOption.Constant:
                return constant;
            case ReferenceOption.Variable:
                return variable;
            default:
                throw new ArgumentOutOfRangeException(selectedReference.ToString() + " is not supported");
        }
    }

    public static implicit operator TConst(ValueReference<TConst, TVar> reference)
    {
        return reference != null
            ? reference.Value
            : default;
    }

    public static TRef Create<TRef>(TConst value)
        where TRef : ValueReference<TConst, TVar>, new()
    {
        var reference = new TRef { constant = value };

        return reference;
    }
}
