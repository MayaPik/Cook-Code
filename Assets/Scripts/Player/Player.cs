using System.Collections;
using UnityEngine;

public abstract class Player : MonoBehaviour //The "abstract" keyword means that this class cannot be instantiated directly, but must be derived by other classes.
{
    protected Animator animator;
    public bool shouldBreakLoop = false;

    [SerializeField] protected bool isMain;

    protected virtual void Start() //The "virtual" keyword allows derived classes to override this method with their own implementation.
    {
        animator = GetComponent<Animator>();
    }

    public abstract IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand);

    protected IEnumerator BreakParentLoop() {
         shouldBreakLoop = true;
    yield return null; // Yielding once to allow the control to return to the GrabItemsCoroutine loop
    }
    protected IEnumerator GrabItemCoroutine(GameObject gameObject, GameObject hand)
    {
        GameObject item = Instantiate(gameObject);
        item.transform.localScale = gameObject.GetComponent<Item>().objectSize;
        item.transform.localPosition = gameObject.GetComponent<Item>().objectLocation;
        item.transform.localRotation = gameObject.GetComponent<Item>().objectRotation;
        CapsuleCollider handCollider = hand.GetComponent<CapsuleCollider>();
        if (handCollider != null)
        {
            item.transform.SetParent(handCollider.transform, false);
        }

        yield return new WaitForSeconds(2f);

        ObjectAction objectAction = item.GetComponent<ObjectAction>();
        if (objectAction != null)
        {
            objectAction.PerformAction();
        }

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Bending") == false);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    protected IEnumerator GoToDestination(GameObject gameObject) {
        Destination[] destinations = FindObjectsOfType<Destination>();
            Destination destination = System.Array.Find(destinations, d => d.tag == gameObject.tag);
            if (destination != null)
            {
                Vector3 destinationLocation = destination.transform.position;
                float movementSpeed = 5f;

                while (transform.position != destinationLocation)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinationLocation, movementSpeed * Time.deltaTime);
                    yield return null;
                }
            }
    }

     
    
}
