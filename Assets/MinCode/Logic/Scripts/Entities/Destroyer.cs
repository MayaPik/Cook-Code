using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys a game object and executes actions afterwards
/// </summary>
public class Destroyer : ActionsExecutor
{
    public void DestroyGameObject()
    {
        Destroy(gameObject);
        ExecuteActions();
    }
}
