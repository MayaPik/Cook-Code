using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(ReferenceSelector))]
public class ReferenceSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        var selectionRect = new Rect(position.x, position.y, 95, position.height);
        var valueRect = new Rect(position.x + 95, position.y, position.width - 95, position.height);

        EditorGUI.PropertyField(selectionRect, property.FindPropertyRelative(nameof(ReferenceSelector.ReferenceValueType)), GUIContent.none);

        DisplayReferenceValueProperty(property, SelectedOption(property), valueRect);

        EditorGUI.EndChangeCheck();
        EditorGUI.EndProperty();
    }

    private ReferenceVariableType SelectedOption(SerializedProperty property) => (ReferenceVariableType)property.FindPropertyRelative(nameof(ReferenceSelector.ReferenceValueType)).enumValueIndex;

    private void DisplayReferenceValueProperty(SerializedProperty property, ReferenceVariableType selection, Rect valueRect)
    {
        var intReferenceProperty = property.FindPropertyRelative(nameof(ReferenceSelector.IntegerReferenceValue));
        var booleanReferenceProperty = property.FindPropertyRelative(nameof(ReferenceSelector.BooleanReferenceValue));
        var floatReferenceProperty = property.FindPropertyRelative(nameof(ReferenceSelector.FloatReferenceValue));
        var stringReferenceProperty = property.FindPropertyRelative(nameof(ReferenceSelector.StringReferenceValue));
        var gameObjectReferenceProperty = property.FindPropertyRelative(nameof(ReferenceSelector.GameObjectReferenceValue));

        switch (selection)
        {
            case ReferenceVariableType.GameObject:
                EditorGUI.PropertyField(valueRect, gameObjectReferenceProperty, GUIContent.none);
                break;
            case ReferenceVariableType.Boolean:
                EditorGUI.PropertyField(valueRect, booleanReferenceProperty, GUIContent.none);
                break;
            case ReferenceVariableType.String:
                EditorGUI.PropertyField(valueRect, stringReferenceProperty, GUIContent.none);
                break;
            case ReferenceVariableType.Integer:
                EditorGUI.PropertyField(valueRect, intReferenceProperty, GUIContent.none);
                break;
            case ReferenceVariableType.Float:
                EditorGUI.PropertyField(valueRect, floatReferenceProperty, GUIContent.none);
                break;
        }
    }
}
