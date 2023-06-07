using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(GameEvent))]
public class GameEventCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myGameEvent = (GameEvent)target;
        GUILayout.Space(25);
        GUILayout.Label($"(Runtime only) Listeners:", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        GUILayout.Label(myGameEvent.ListenersNames(separateLines: true));

        if (GUILayout.Button("Raise Event"))
        {
            myGameEvent.Raise();
        }
    }
}

