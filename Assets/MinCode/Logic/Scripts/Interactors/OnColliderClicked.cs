using System.Collections.Generic;
using UnityEngine.EventSystems;

public class OnColliderClicked : ActionsExecutor
{
    public List<ClickMode> clickModes;

    private void OnMouseDown()
    {
        PrintDebugInfo($"OnColliderClicked.OnMouseDown. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseDown))
        {
            Handle();
        }
    }

    public void OnMouseUpAsButton()
    {
        PrintDebugInfo($"OnColliderClicked.OnMouseUpAsButton. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseUpAsButton))
        {
            Handle();
        }
    }

    private void OnMouseUp()
    {
        PrintDebugInfo($"OnColliderClicked.OnMouseUp. clickMode: {ClickModesToString()}");

        if (clickModes.Contains(ClickMode.MouseUp))
        {
            Handle();
        }
    }

    private void Handle()
    {
        PrintDebugInfo($"OnColliderClicked - Handling collider click. is click on UI: {EventSystem.current.IsPointerOverGameObject()}");

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ExecuteActions();
        }
    }

    private string ClickModesToString()
    {
        return string.Join(", ", clickModes);
    }
}
