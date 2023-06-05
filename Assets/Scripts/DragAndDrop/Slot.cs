using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public GameObject draggedObject;
    public SpriteRenderer spriteRenderer;
    public Sprite regularSprite;
    public Sprite hoverSprite;
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
        regularSprite = spriteRenderer.sprite;
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
                dragged.transform.position = transform.position;
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
