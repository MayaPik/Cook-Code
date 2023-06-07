using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Vector3Reference))]
public class Vector3ReferenceDrawer : ValueReferenceDrawer<Vector3Reference, Vector3, Vector3Variable>
{
}