using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

[Serializable]
public class MouseInputListener
{
    public MouseMode mouseMode;
    public BooleanReference executeActions;
    public UnityEvent actions;
    public Func<int, bool> inputDelegate { get; set; }
}

[Serializable]
public enum MouseMode
{
    MouseUp,
    MouseDown,
    MouseButtonDown
}

public abstract class OnMouseInputBase : BehaviorBase
{
    public List<MouseInputListener> mouseInputListeners;
    public BooleanReference executeActions;

    public void DontExecuteAction(int index)
    {
        if (mouseInputListeners != null && index >= 0 && index < mouseInputListeners.Count)
        {
            mouseInputListeners.ElementAt(index).executeActions = BooleanReference.Create(false);
        }
        else
        {
            Debug.LogError($"invalid index sent to OnMouseInputBase.SetExecuteAction: {index} or object didn't initialized");
        }
    }

    protected abstract void OnMouseInput(MouseInputListener listener);

    private void Start()
    {
        foreach (var listener in mouseInputListeners)
        {
            switch (listener.mouseMode)
            {
                case MouseMode.MouseButtonDown:
                    listener.inputDelegate = Input.GetMouseButtonDown;
                    break;
                case MouseMode.MouseDown:
                    listener.inputDelegate = Input.GetMouseButton;
                    break;
                case MouseMode.MouseUp:
                    listener.inputDelegate = Input.GetMouseButtonUp;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{listener.mouseMode} is not handled ({name})");
            }
        }
    }

    private void Update()
    {
        if (executeActions && !EventSystem.current.IsPointerOverGameObject())
        {
            foreach (var listener in mouseInputListeners.Where(input => input.executeActions && input.inputDelegate.Invoke(0)))
            {
                PrintDebugInfo($"detected mouse input {listener.mouseMode}");
                OnMouseInput(listener);
            }
        }
    }
}
