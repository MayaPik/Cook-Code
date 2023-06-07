using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class RuntimeSet<T> : ScriptableObject where T : UnityEngine.Object
{
    public List<T> Items = new List<T>();
    public UnityEvent OnItemAdded;
    public UnityEvent OnItemRemoved;
    public string ItemsNames(bool separateLines = false) => Items == null ? "No listeners!" : string.Join(separateLines ? $", {Environment.NewLine}" : ", ", Items.Select(l => l?.name));
    public int TotalObjectsAdded => allItemsHashCodes.Count;
    public bool ForceUniquness = false;
    public bool PrintDebugInfo;
    private HashSet<int> itemsHashCodes = new HashSet<int>();
    private HashSet<string> allItemsHashCodes = new HashSet<string>();

    public RuntimeSet()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        itemsHashCodes = new HashSet<int>();
        allItemsHashCodes = new HashSet<string>();
    }

    public void Set(List<T> items)
    {
        Reset();

        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void Reset()
    {
        if (itemsHashCodes != null)
        {
            itemsHashCodes.Clear();
        }

        if (allItemsHashCodes != null)
        {
            allItemsHashCodes.Clear();
        }

        if (Items != null)
        {
            Items.Clear();
        }
    }

    public void Add(T toAdd)
    {
        if (toAdd != null)
        {
            var toAddId = toAdd.GetInstanceID();

            if (!ForceUniquness)
            {
                if (PrintDebugInfo)
                {
                    Debug.Log($"Adding {toAdd.ToString()} with ID: {toAddId} to {name}");
                }

                Items.Add(toAdd);
                OnItemAdded.Invoke();
            }
            else if (!itemsHashCodes.Contains(toAddId))
            {
                if (PrintDebugInfo)
                {
                    Debug.Log($"Adding {toAdd.ToString()} with ID: {toAddId} to {name}");
                }

                Items.Add(toAdd);
                itemsHashCodes.Add(toAddId);
                OnItemAdded.Invoke();

                if (!allItemsHashCodes.Contains(toAdd.name))
                {
                    allItemsHashCodes.Add(toAdd.name);
                }
            }
        }
    }

    public void Remove(T toRemove)
    {
        if (toRemove != null)
        {
            var toRemoveId = toRemove.GetInstanceID();

            if (!ForceUniquness && Items.Contains(toRemove))
            {
                if (PrintDebugInfo)
                {
                    Debug.Log($"Removing {toRemove.ToString()} to {name}");
                }

                Items.Remove(toRemove);
                OnItemRemoved.Invoke();
            }
            else if (itemsHashCodes.Contains(toRemoveId))
            {
                if (PrintDebugInfo)
                {
                    Debug.Log($"Removing {toRemove.ToString()} to {name}");
                }

                Items.Remove(toRemove);
                itemsHashCodes.Remove(toRemoveId);
                OnItemRemoved.Invoke();
            }
        }
    }
}
