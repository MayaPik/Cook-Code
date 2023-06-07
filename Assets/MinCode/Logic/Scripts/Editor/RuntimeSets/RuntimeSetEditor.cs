using UnityEngine;
using UnityEditor;

/// <summary>
/// Holds a set of objects in runtime
/// </summary>
/// <typeparam name="TSet">The runtime set type</typeparam>
/// <typeparam name="TVal">The value of the generic RuntimeSet</typeparam>
public class RuntimeSetEditor<TSet, TVal> : Editor
    where TSet : RuntimeSet<TVal>
    where TVal : Object
{
    private TSet myTarget;

    private SerializedObject serializedObjectTarget;
    private SerializedProperty printDebugProperty;
    private SerializedProperty forceUniqunessProperty;
    private SerializedProperty onItemAddedProperty;
    private SerializedProperty onItemRemovedProperty;

    private void OnEnable()
    {
        myTarget = (TSet)target;
        serializedObjectTarget = new SerializedObject(target);

        printDebugProperty = serializedObjectTarget.FindProperty(nameof(RuntimeSet<TVal>.PrintDebugInfo));
        forceUniqunessProperty = serializedObjectTarget.FindProperty(nameof(RuntimeSet<TVal>.ForceUniquness));
        onItemAddedProperty = serializedObjectTarget.FindProperty(nameof(RuntimeSet<TVal>.OnItemAdded));
        onItemRemovedProperty = serializedObjectTarget.FindProperty(nameof(RuntimeSet<TVal>.OnItemRemoved));
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(printDebugProperty);
        EditorGUILayout.PropertyField(forceUniqunessProperty);
        GUILayout.Space(25);
        GUILayout.Label($"Actions", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(onItemAddedProperty);
        EditorGUILayout.PropertyField(onItemRemovedProperty);

        if (EditorGUI.EndChangeCheck())
        {
            serializedObjectTarget.ApplyModifiedProperties();
        }

        GUILayout.Space(25);
        GUILayout.Label($"(Runtime only) Items names:", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        GUILayout.Label(myTarget.ItemsNames(separateLines: true));
    }
}
