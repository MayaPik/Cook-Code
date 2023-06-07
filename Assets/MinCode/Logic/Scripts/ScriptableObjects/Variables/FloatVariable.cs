using UnityEngine;

[CreateAssetMenu(menuName = "Custom Variables/Float")]
public class FloatVariable : VariableScriptableObject<float>, IArithmeticableVariable<float>
{
    public void Add(float toAdd)
    {
        Value += toAdd;
    }

    public void Divide(float toDivide)
    {
        Value /= toDivide;
    }

    public void Multiply(float toMultiply)
    {
        Value *= toMultiply;
    }

    public void Substract(float toSubstract)
    {
        Value -= toSubstract;
    }

    public override string ToString()
    {
        return Value.ToString("F5");
    }
}
