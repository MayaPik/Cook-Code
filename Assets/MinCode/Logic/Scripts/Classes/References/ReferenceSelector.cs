using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A component that lets you select a type and its value. Notice that it has a custom editor, that lets you choose the type and value
/// </summary>
[Serializable]
public class ReferenceSelector
{
    public IntegerReference IntegerReferenceValue;
    public BooleanReference BooleanReferenceValue;
    public FloatReference FloatReferenceValue;
    public StringReference StringReferenceValue;
    public GameObjectReference GameObjectReferenceValue;
    public ReferenceVariableType ReferenceValueType;

    public T GetReferenceValue<T>(bool printDebugInfo)
    {
        var refValue = GetReferenceValue(printDebugInfo);

        return refValue is T refValueT ? refValueT : default;
    }

    public object GetReferenceValue(bool printDebugInfo)
    {
        object result = null;
        var referenceName = string.Empty;

        switch (ReferenceValueType)
        {
            case ReferenceVariableType.Boolean:
                result = BooleanReferenceValue?.Value;
                referenceName = BooleanReferenceValue?.Name;
                break;
            case ReferenceVariableType.String:
                result = StringReferenceValue?.Value;
                referenceName = StringReferenceValue?.Name;
                break;
            case ReferenceVariableType.Float:
                result = FloatReferenceValue?.Value;
                referenceName = FloatReferenceValue?.Name;
                break;
            case ReferenceVariableType.Integer:
                result = IntegerReferenceValue?.Value;
                referenceName = IntegerReferenceValue?.Name;
                break;
            case ReferenceVariableType.GameObject:
                result = GameObjectReferenceValue?.Value;
                referenceName = GameObjectReferenceValue?.Name;
                break;
            default:
                throw new ArgumentOutOfRangeException(ReferenceValueType.ToString());
        }

        if (printDebugInfo)
        {
            Debug.Log($"{nameof(GetReferenceValue)} returned reference '{referenceName}' of type {result?.GetType().Name} with value {result}");
        }

        return result;
    }

    public void SetValueChangeListener(GameObject listener, UnityAction callback, bool printDebugInfo)
    {
        var printDebugInfoRef = BooleanReference.Create(printDebugInfo);

        switch (ReferenceValueType)
        {
            case ReferenceVariableType.Integer:
                IntegerReferenceValue.CreateChangeEventListenerIfThereIs(listener, printDebugInfoRef, callback);
                break;
            case ReferenceVariableType.Boolean:
                BooleanReferenceValue.CreateChangeEventListenerIfThereIs(listener, printDebugInfoRef, callback);
                break;
            case ReferenceVariableType.String:
                StringReferenceValue.CreateChangeEventListenerIfThereIs(listener, printDebugInfoRef, callback);
                break;
            case ReferenceVariableType.Float:
                FloatReferenceValue.CreateChangeEventListenerIfThereIs(listener, printDebugInfoRef, callback);
                break;
            case ReferenceVariableType.GameObject:
                GameObjectReferenceValue.CreateChangeEventListenerIfThereIs(listener, printDebugInfoRef, callback);
                break;
            default:
                throw new ArgumentOutOfRangeException(ReferenceValueType.ToString());
        }
    }
}
