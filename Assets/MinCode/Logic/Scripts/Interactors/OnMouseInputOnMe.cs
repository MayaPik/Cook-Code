using UnityEngine;

public class OnMouseInputOnMe : OnMouseInputBase
{
    private int myId;

    protected virtual void Awake()
    {
        myId = gameObject.GetInstanceID();
    }

    protected override void OnMouseInput(MouseInputListener listener)
    {
        if (listener?.executeActions)
        {
            var mouseRay = Physics2D.GetRayIntersection(TouchInput.GetInputRay(), Mathf.Infinity);

            PrintDebugInfo($"OnMouseInputOnMe: ray collider: {mouseRay.collider?.name}, collider id: {mouseRay.collider?.gameObject.GetInstanceID()}, my id: {myId}");

            if (mouseRay.collider != null)
            {
                if (myId == mouseRay.collider.gameObject.GetInstanceID())
                {
                    listener.actions?.Invoke();
                }
            }
        }
    }
}
