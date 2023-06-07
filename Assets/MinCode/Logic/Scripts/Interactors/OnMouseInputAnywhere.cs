using UnityEngine;

public class OnMouseInputAnywhere : OnMouseInputBase
{
    public bool ignoreLayer = true;
    public LayerMask layer = ~0;

    protected override void OnMouseInput(MouseInputListener listener)
    {
        if (listener?.executeActions)
        {
            var mouseRay = TouchInput.GetInputRay();

            if (ignoreLayer || Physics.Raycast(mouseRay, Mathf.Infinity, layer))
            {
                listener.actions?.Invoke();
            }
        }
    }
}

