using System;
using UnityEngine;

[Serializable]
public class GameObjectReference : ValueReference<GameObject, GameObjectVariable>
{
    public Vector3 position => Value.transform.position;

    public override string ToString()
    {
        return Value != null ? Value.name : string.Empty;
    }
}
