using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameObjectReference))]
public class GameObjectReferenceDrawer : ValueReferenceDrawer<GameObjectReference, GameObject, GameObjectVariable>
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var optionRect = new Rect(position.x, position.y, 70, position.height);
        var valueRect = new Rect(position.x + 75, position.y, position.width - 75, position.height);

        EditorGUI.PropertyField(optionRect, property.FindPropertyRelative(nameof(GameObjectReference.selectedReference)), GUIContent.none);

        switch (SelectedOption(property))
        {
            case ReferenceOption.Constant:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(GameObjectReference.constant)), GUIContent.none);
                break;
            case ReferenceOption.Variable:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(GameObjectReference.variable)), GUIContent.none);
                break;
            default:
                break;
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    private ReferenceOption SelectedOption(SerializedProperty property) => (ReferenceOption)property.FindPropertyRelative(nameof(FloatReference.selectedReference)).enumValueIndex;
}
