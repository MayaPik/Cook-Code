using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera fpsCamera;
    private Vector3 offset;
    private Slot previousSlot;
    private Slot hitSlot;
    public Vector3 originalPosition;
    private bool hover;
    public PlaySound playSound; // Reference to the PlaySound script

    private void Awake()
    {
        playSound = FindObjectOfType<PlaySound>();
        fpsCamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
    }

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
    else if (hitSlot != null)
    {
        hitSlot.HoverColor(false);
        hitSlot = null;
        hover = false;
    }
}

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = fpsCamera.WorldToScreenPoint(transform.position).z;
        return fpsCamera.ScreenToWorldPoint(mouseScreenPos);
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

        Ray ray = fpsCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            Slot newHitSlot = hitInfo.collider.GetComponent<Slot>();
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
    playSound.TriggerSoundEvent();
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
    else if (previousSlot != null)
    {
        previousSlot.EmptySlot();
        previousSlot = null;
    }
    else
    {
        transform.position = originalPosition;
    }
}

}
