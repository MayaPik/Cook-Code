using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public GameObject draggedObject;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite regularSprite;
    [SerializeField] Sprite hoverSprite;

    public enum TypeOptions
        {
            Normal,
            Abstract
        }

        [SerializeField]
        public TypeOptions type;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer) {
        regularSprite = spriteRenderer.sprite;
        }
    }

    public void PlaceItemInSlot(GameObject gameObject)
    {
        draggedObject = gameObject;
    }

    public void OnDrop(GameObject dragged)
    {
        if (draggedObject)
        {
            dragged.transform.position = dragged.GetComponent<DragAndDrop>().originalPosition;
        }
        else
        {
            if (dragged != null)
            {
                Vector3 position = transform.position;
                position.y = dragged.transform.position.y; // Set the Y-axis position to match the slot's Y-axis
                dragged.transform.position = position;
                draggedObject = dragged;
            }
        }
    }

    public void EmptySlot()
    {
        draggedObject = null;
    }

   public void HoverColor(bool standby)
{
    if (spriteRenderer != null && regularSprite != null && hoverSprite != null)
    {
        if (standby)
        {
            spriteRenderer.sprite = hoverSprite;
        }
        else
        {
            spriteRenderer.sprite = regularSprite;
        }
    }
}

}
