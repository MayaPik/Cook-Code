using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickOutsideCollider : ActionsExecutor
{
    public List<ClickMode> clickModes;
    private bool clickWasOnMe;

    private void Update()
    {
        if (!clickWasOnMe && Input.GetMouseButtonDown(0))
        {
            ExecuteActions();
        }

        clickWasOnMe = false;
    }

    private void OnMouseDown()
    {
        PrintDebugInfo($"OnClickOutsideCollider OnMouseDown. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseDown))
        {
            Handle();
        }
    }

    public void OnMouseUpAsButton()
    {
        PrintDebugInfo($"OnClickOutsideCollider OnMouseUpAsButton. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseUpAsButton))
        {
            Handle();
        }
    }

    private void OnMouseUp()
    {
        PrintDebugInfo($"'OnClickOutsideCollider OnMouseUp. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseUp))
        {
            Handle();
        }
    }

    private void Handle()
    {
        PrintDebugInfo($"'{name}' OnClickOutsideCollider Handling collider click. is click on UI: {EventSystem.current.IsPointerOverGameObject()}");

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            clickWasOnMe = true;
        }
    }

    private string ClickModesToString()
    {
        return string.Join(", ", clickModes);
    }
}
