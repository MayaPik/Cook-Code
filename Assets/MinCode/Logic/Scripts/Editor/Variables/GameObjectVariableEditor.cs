using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameObjectVariable))]
public class GameObjectVariableEditor : ValueVariableEditor<GameObjectVariable, GameObject>
{
}
