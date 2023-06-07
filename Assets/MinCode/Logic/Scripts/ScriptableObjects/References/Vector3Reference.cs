using System;
using UnityEngine;

[Serializable]
public class Vector3Reference : ValueReference<Vector3, Vector3Variable>
{
    public override string ToString()
    {
        return Value.ToString("F5");
    }
}
