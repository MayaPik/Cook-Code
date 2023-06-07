using System;

[Serializable]
public class FloatReference : ValueReference<float, FloatVariable>
{
    public override string ToString()
    {
        return Value.ToString("F5");
    }
}
