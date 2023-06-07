using UnityEditor;

[CustomPropertyDrawer(typeof(IntegerReference))]
public class IntegerReferenceDrawer : ValueReferenceDrawer<IntegerReference, int, IntegerVariable>
{
}
