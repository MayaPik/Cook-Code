using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public enum CollisionTriggerMode
{
    Enter,
    Exit,
    Stay
}

/// <summary>
/// Executes an action once a collision happened
/// </summary>
public class ActionOnCollisionTrigger : ActionsExecutor
{
    public List<CollisionTriggerMode> modes;
    public List<string> considerTags;
    private HashSet<CollisionTriggerMode> _modesHashSet;

    private void Start()
    {
        if (modes.IsNullOrEmpty())
        {
            modes = new List<CollisionTriggerMode>();

            foreach (var value in Enum.GetValues(typeof(CollisionTriggerMode)))
            {
                modes.Add((CollisionTriggerMode)value);
            }
        }

        _modesHashSet = new HashSet<CollisionTriggerMode>(modes);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Enter))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Enter))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Enter))
        {
            HandleCollisionTrigger(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Stay))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Stay))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Exit))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_modesHashSet.Contains(CollisionTriggerMode.Exit))
        {
            HandleCollisionTrigger(collision.gameObject);
        }
    }

    private void HandleCollisionTrigger(GameObject collider)
    {
        if (IsTransformConsidered(collider.transform))
        {
            ExecuteActions($"Collision/trigger with: {collider.name}. modes: {string.Join(", ", _modesHashSet)}");
        }
    }

    private bool IsTransformConsidered(Transform colliderTransform)
    {
        if (considerTags.IsNullOrEmpty())
        {
            return true;
        }

        return considerTags.Contains(colliderTransform.tag);
    }
}
