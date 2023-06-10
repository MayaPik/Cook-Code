using UnityEngine;
using UnityEngine.Events;

public class MouseDownButton : MonoBehaviour
{
    public UnityEvent onMouseDownEvent;

    private void OnMouseDown()
    {
        onMouseDownEvent.Invoke();
        
    }
}
