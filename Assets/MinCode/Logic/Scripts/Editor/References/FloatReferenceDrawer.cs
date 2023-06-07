using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : ValueReferenceDrawer<FloatReference, float, FloatVariable>
{
}
