using UnityEditor;
using UnityEngine;

/// <summary>
/// Generic implementation for reference variables property drawer
/// </summary>
/// <typeparam name="TDraw">The reference type to draw</typeparam>
/// <typeparam name="TConst">The reference underlying type</typeparam>
/// <typeparam name="TVar">The reference variable type</typeparam>
public abstract class ValueReferenceDrawer<TDraw, TConst, TVar> : PropertyDrawer
    where TVar : VariableScriptableObject<TConst>
    where TDraw : ValueReference<TConst, TVar>
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

        EditorGUI.PropertyField(optionRect, property.FindPropertyRelative(nameof(ValueReference<TConst, TVar>.selectedReference)), GUIContent.none);

        switch (SelectedOption(property))
        {
            case ReferenceOption.Constant:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(ValueReference<TConst, TVar>.constant)), GUIContent.none);
                break;
            case ReferenceOption.Variable:
                EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(nameof(ValueReference<TConst, TVar>.variable)), GUIContent.none);
                break;
            default:
                break;
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndChangeCheck();
        EditorGUI.EndProperty();
    }

    private ReferenceOption SelectedOption(SerializedProperty property) => (ReferenceOption)property.FindPropertyRelative(nameof(FloatReference.selectedReference)).enumValueIndex;
}
