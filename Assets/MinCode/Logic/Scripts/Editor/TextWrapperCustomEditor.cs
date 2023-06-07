using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(TextWrapper))]
public class TextWrapperCustomEditor : Editor
{
    private TextWrapper myTarget;
    private SerializedObject serializedObjectTarget;
    private SerializedProperty referenceProperty;
    private SerializedProperty prefixProperty;
    private SerializedProperty suffixProperty;
    private SerializedProperty textProperty;

    private void OnEnable()
    {
        myTarget = (TextWrapper)target;
        serializedObjectTarget = new SerializedObject(target);

        referenceProperty = serializedObjectTarget.FindProperty(nameof(TextWrapper.Reference));
        prefixProperty = serializedObjectTarget.FindProperty(nameof(TextWrapper.Prefix));
        suffixProperty = serializedObjectTarget.FindProperty(nameof(TextWrapper.Suffix));
        textProperty = serializedObjectTarget.FindProperty(nameof(TextWrapper.Text));
    }

    public override void OnInspectorGUI()
    {
        serializedObjectTarget.Update();
        EditorGUI.BeginChangeCheck();
        GUILayout.Space(25);

        DisplayTextProperties();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObjectTarget.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Update Text (RUNTIME)"))
        {
            myTarget.UpdateText();
        }
    }

    private void DisplayTextProperties()
    {
        GUILayout.Label("Textual properties of the text", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(referenceProperty);
        EditorGUILayout.PropertyField(textProperty);
        EditorGUILayout.PropertyField(prefixProperty);
        EditorGUILayout.PropertyField(suffixProperty);
    }
}