using UnityEditor;

[CustomPropertyDrawer(typeof(StringReference))]
public class StringReferenceDrawer : ValueReferenceDrawer<StringReference, string, StringVariable>
{
}