using System;
using UnityEngine;

[Serializable]
public class BooleanReference : ValueReference<bool, BooleanVariable>
{
    public static BooleanReference Create(bool value) => Create<BooleanReference>(value);
}
