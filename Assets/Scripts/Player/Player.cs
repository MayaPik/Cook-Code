using System.Collections;
using UnityEngine;

public abstract class Player : MonoBehaviour //The "abstract" keyword means that this class cannot be instantiated directly, but must be derived by other classes.
{
    protected Animator animator;
    public bool shouldBreakLoop = false;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    
    [SerializeField] protected bool isMain;

    protected virtual void Start() //The "virtual" keyword allows derived classes to override this method with their own implementation.
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;

    }

    public abstract IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand);

    protected IEnumerator BreakParentLoop() {
    shouldBreakLoop = true;
    transform.position = originalPosition;
    yield return null; // Yielding once to allow the control to return to the GrabItemsCoroutine loop
    }

   protected IEnumerator GoToLocation(Vector3 location)
{
    float movementSpeed = 1f;
    float stoppingDistance = 1f; // Adjust this value as needed

    Quaternion targetRotation = Quaternion.LookRotation(location - transform.position);
    transform.rotation = targetRotation;

    while (Vector3.Distance(transform.position, location) >= stoppingDistance)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, movementSpeed * Time.deltaTime);
        yield return null;
    }

}
    protected IEnumerator GrabItemCoroutine(GameObject item, GameObject hand)
    {
        CapsuleCollider handCollider = hand.GetComponent<CapsuleCollider>();
       if (handCollider != null)
    {
        if (item.transform.parent != handCollider.transform)
        {
            item.transform.SetParent(handCollider.transform, false);
        }
    }
        // particle.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ObjectAction objectAction = item.GetComponent<ObjectAction>();
        if (objectAction != null)
        {
            objectAction.PerformAction();
        }

        // particle.gameObject.SetActive(false);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Bending") == false);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

  protected IEnumerator GoToDestination(GameObject gameObject)
{
    Destination[] destinations = FindObjectsOfType<Destination>();
    Destination destination = System.Array.Find(destinations, d => d.tag == gameObject.tag);
    if (destination != null)
    {
        Vector3 destinationLocation = destination.transform.position;
        float movementSpeed = 5f;
        float stoppingDistance = 3.5f;

        // Rotate player towards the destination before moving
        Quaternion targetRotation = Quaternion.LookRotation(destinationLocation - transform.position);
        transform.rotation = targetRotation;

        while (Vector3.Distance(transform.position, destinationLocation) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationLocation, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

}



     
    
