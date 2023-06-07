using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Generic editor for value variables
/// </summary>
/// <typeparam name="TScriptable">Sciptable object type</typeparam>
/// <typeparam name="TVal">Value type</typeparam>
public class ValueVariableEditor<TScriptable, TVal> : Editor where TScriptable : VariableScriptableObject<TVal>
{
    private TScriptable myTarget;

    private SerializedObject serializedObjectTarget;
    private SerializedProperty valueProperty;
    private SerializedProperty initialValueProperty;
    private SerializedProperty changeEventProperty;
    private SerializedProperty changeActionsProperty;
    private SerializedProperty changeSpecificValueActionsProperty;
    private SerializedProperty printDebugProperty;
    private SerializedProperty storeOnDbProperty;

    private void OnEnable()
    {
        myTarget = (TScriptable)target;
        serializedObjectTarget = new SerializedObject(target);

        valueProperty = serializedObjectTarget.FindProperty("_value");
        initialValueProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.ResetValue));
        changeEventProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.EventOnChange));
        changeActionsProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.ActionsOnChange));
        changeSpecificValueActionsProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.SpecificValuesActions));
        printDebugProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.PrintDebugInfo));
        storeOnDbProperty = serializedObjectTarget.FindProperty(nameof(VariableScriptableObject<TVal>.StoreOnDb));

        if (valueProperty == null)
        {
            throw new ArgumentException($"FATAL ERROR: Can't find _value member on class of type: {typeof(TScriptable).Name}");
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(valueProperty);
        GUILayout.Space(25);
        GUILayout.Label("Optional", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(printDebugProperty);
        EditorGUILayout.PropertyField(storeOnDbProperty);

        GUILayout.Space(15);
        EditorGUILayout.PropertyField(changeEventProperty);
        EditorGUILayout.PropertyField(changeActionsProperty);
        EditorGUILayout.PropertyField(changeSpecificValueActionsProperty);
        GUILayout.Space(15);
        myTarget.HasInitialValue = GUILayout.Toggle(myTarget.HasInitialValue, "Set value on reset");

        if (myTarget.HasInitialValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(initialValueProperty);
            EditorGUI.indentLevel--;
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObjectTarget.ApplyModifiedProperties();
            myTarget.EventOnChange.RaiseIfNotNull();
        }
    }
}
