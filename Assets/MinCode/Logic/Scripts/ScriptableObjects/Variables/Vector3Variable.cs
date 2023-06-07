using UnityEngine;

[CreateAssetMenu(menuName = "Custom Variables/Vector3")]
public class Vector3Variable : VariableScriptableObject<Vector3>
{
    public void SetToGameObjectPosition(GameObject obj)
    {
        if (obj != null)
        {
            SetValue(obj.transform.position);
        }
    }

    public override string ToString()
    {
        return Value.ToString("F5");
    }
}
