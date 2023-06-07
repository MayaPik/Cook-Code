using UnityEditor;

[CustomPropertyDrawer(typeof(BooleanReference))]
public class BooleanReferenceDrawer : ValueReferenceDrawer<BooleanReference, bool, BooleanVariable>
{
}
