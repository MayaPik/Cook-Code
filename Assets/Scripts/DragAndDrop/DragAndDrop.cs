using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Camera codeCamera;
    private Vector3 offset;
    private Slot previousSlot;
    private Slot hitSlot;
    public Vector3 originalPosition;
    public bool hover;

    private void Start()
    {
        hover = false;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (hitSlot)
        {
            hitSlot.HoverColor(hover);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = codeCamera.WorldToScreenPoint(transform.position).z;
        return codeCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        offset.z = 0f;
        transform.GetComponent<BoxCollider>().enabled = false;
    }


    private void OnMouseDrag()
    {
        Vector3 targetPosition = GetMouseWorldPosition() + offset;
        targetPosition.z = transform.position.z;
        transform.position = targetPosition;
        var rayOrigin = codeCamera.transform.position;
        var rayDirection = GetMouseWorldPosition() - codeCamera.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            var newHitSlot = hitInfo.transform.GetComponent<Slot>();
            if (newHitSlot != null && newHitSlot != hitSlot)
            {
                if (hitSlot != null)
                {
                    hitSlot.HoverColor(false);
                }

                hitSlot = newHitSlot;
                targetPosition = hitSlot.transform.position;
                targetPosition.z = transform.position.z;
                transform.position = targetPosition;
                hover = true;
            }
        }
        else if (hitSlot != null)
        {
            hitSlot.HoverColor(false);
            hitSlot = null;
            hover = false;
        }
    }

    private void OnMouseUp()
    {
        transform.GetComponent<BoxCollider>().enabled = true;
        hover = false;

        if (hitSlot != null)
        {
            if (previousSlot != null && previousSlot != hitSlot)
            {
                previousSlot.EmptySlot();
            }
            hitSlot.OnDrop(gameObject);
            previousSlot = hitSlot;
        }
        else
        {
            transform.position = originalPosition;
            if (previousSlot != null)
            {
                previousSlot.EmptySlot();
                previousSlot = null;
            }
        }
    }
}