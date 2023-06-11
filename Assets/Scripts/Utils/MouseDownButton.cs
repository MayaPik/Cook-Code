using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class MouseDownButton : MonoBehaviour
{
    public UnityEvent onMouseDownEvent;
    public bool locked = false;
    public TextMeshPro text;

    private void OnMouseDown()
    {
        if (!locked)
        {
            onMouseDownEvent.Invoke();
        }
        else
        {
            text.text = "This Level is still locked";
        }
    }
}
