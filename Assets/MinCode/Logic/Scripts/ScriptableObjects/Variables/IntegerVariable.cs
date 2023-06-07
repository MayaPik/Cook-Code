using UnityEngine;

[CreateAssetMenu(menuName = "Custom Variables/Integer")]
public class IntegerVariable : VariableScriptableObject<int>, IArithmeticableVariable<int>
{
    public void Add(int toAdd)
    {
        Value += toAdd;
    }

    public void Divide(int toDivide)
    {
        Value /= toDivide;
    }

    public void Multiply(int toMultiply)
    {
        Value *= toMultiply;
    }

    public void Substract(int toSubstract)
    {
        Value -= toSubstract;
    }
}
