using UnityEngine;
using System.Collections;


/// <summary>
/// Look at a target game object
/// </summary>
public class LookAt : MonoBehaviour
{
    public BooleanReference isInGame;
    public BooleanReference isLooking;
    public Transform looker;
    public Transform target;
    public Vector3 worldUp = Vector3.up;
    private Vector3 eulerAnglesOnStart;

    private void Start()
    {
        eulerAnglesOnStart = looker.eulerAngles;
    }

    public void SetIsLooking(bool newValue)
    {
        if (isLooking && !newValue)
        {
            looker.eulerAngles = eulerAnglesOnStart;
        }

        if (newValue)
        {
            eulerAnglesOnStart = looker.eulerAngles;
        }

        isLooking.SetValue(newValue);
    }

    void Update()
    {
        if (isLooking && isInGame)
        {
            looker.LookAt(target, worldUp);
        }
    }
}
