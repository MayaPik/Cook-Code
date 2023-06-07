using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ActionsWhenValueIs<T>
{
    public T value;
    public UnityEvent action;
}

/// <summary>
/// The most basic entity for this package - allows you to control your variables
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class VariableScriptableObject<T> : ScriptableObject
{
    [SerializeField]
    private T _value;

    public UnityEvent ActionsOnChange;
    public List<ActionsWhenValueIs<T>> SpecificValuesActions;
    public GameEvent EventOnChange;
    public GameEvent EventOnSet;
    [HideInInspector]
    public bool HasInitialValue;
    public bool PrintDebugInfo;
    [Tooltip("Variable value to set on reset")]
    public T ResetValue;
    public bool StoreOnDb;

    public T Value
    {
        get => GetValue();
        set => SetValue(value);
    }

    public T GetValue()
    {
        return _value;
    }

    public void SetValue(T value)
    {
        bool isChanged = _value == null ? value == null : !_value.Equals(value);

        if (PrintDebugInfo)
        {
            Debug.Log($"Setting '{name}' from {_value} to {value}. EventOnChange: {EventOnChange?.name}, isChanged: {isChanged}");
        }

        _value = value;

        EventOnSet.RaiseIfNotNull();

        if (isChanged)
        {
            SetValueOnDb();
            EventOnChange.RaiseIfNotNull();
            ActionsOnChange?.Invoke();
        }

        var valueActions = SpecificValuesActions
                .Where(valueAction => valueAction.value.Equals(_value))
                .ToList();

        if (valueActions.Any())
        {
            if (PrintDebugInfo)
            {
                Debug.Log($"Found an action for when '{name}' value is '{_value}', executing actions");
            }

            valueActions.ForEach(valueAction => valueAction.action.Invoke());
        }
    }

    public void SetValueSilently(T value)
    {
        if (PrintDebugInfo)
        {
            Debug.Log($"Setting '{name}' from {_value} to {value} SILENTLY");
        }

        _value = value;
    }

    public void Reset()
    {
        if (PrintDebugInfo)
        {
            Debug.Log($"Resetting '{name}'. Value: {Value}, ResetValue: {ResetValue}, HasInitialValue: {HasInitialValue}");
        }


        if (HasInitialValue && !StoreOnDb)
        {
            Value = ResetValue;
        }

        if (StoreOnDb)
        {
            ResetValueToValueFromDb();
        }
    }

    public override string ToString()
    {
        return Value?.ToString();
    }

    public static implicit operator T(VariableScriptableObject<T> variable) => variable.Value;

    private void OnEnable()
    {
        Reset();
    }

    private void OnDisable()
    {
        Reset();
    }

    private void SetValueOnDb()
    {
        if (StoreOnDb)
        {
            if (_value is int valueInt)
            {
                PlayerPrefs.SetInt(name, valueInt);
            }
            else if (_value is float valueFloat)
            {
                PlayerPrefs.SetFloat(name, valueFloat);
            }
            else if (_value is string valueString)
            {
                PlayerPrefs.SetString(name, valueString);
            }
            else if (_value is bool valueBool)
            {
                PlayerPrefs.SetInt(name, valueBool ? 1 : 0);
            }
        }
    }

    private void ResetValueToValueFromDb()
    {
        if (_value is int)
        {
            var defaultIntValue = int.TryParse(ResetValue?.ToString(), out var defaultInt) ? defaultInt : default;
            SetValue((T)(object)PlayerPrefs.GetInt(name, defaultIntValue));
        }
        else if (_value is float)
        {
            var defaultFloatValue = float.TryParse(ResetValue?.ToString(), out var defaultFloat) ? defaultFloat : default;
            SetValue((T)(object)PlayerPrefs.GetFloat(name, defaultFloatValue));
        }
        else if (_value is string)
        {
            var defaultStringValue = !string.IsNullOrEmpty(ResetValue?.ToString()) ? ResetValue.ToString() : default;
            SetValue((T)(object)PlayerPrefs.GetString(name, defaultStringValue));
        }
        else if (_value is bool)
        {
            int resetBoolValue = 0;

            if (ResetValue is bool boolResetValue)
            {
                resetBoolValue = boolResetValue ? 1 : 0;
            }

            var valueOnDb = PlayerPrefs.GetInt(name, resetBoolValue);
            var boolValue = valueOnDb == 1 ? true : false;

            SetValue((T)(object)boolValue);
        }
    }
}
