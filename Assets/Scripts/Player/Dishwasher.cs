using System.Collections;
using UnityEngine;

public class Dishwasher : Player
{
    [SerializeField] GameObject destination;
    [SerializeField] GameObject poolOfObjects;

    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
       if (slot.gameObject.GetComponent<PlayerDebugged>().enabled)
       {

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        animator.SetTrigger("Idle");
        animator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f);
        GameObject item = itemGameObject;
        Item itemComponent = item.GetComponent<Item>();
        item.transform.localScale = itemComponent.ObjectSize;
        item.transform.localPosition = itemComponent.ObjectLocation;
        item.transform.localRotation = itemComponent.ObjectRotation;
        yield return StartCoroutine(GrabItemCoroutine(item, hand));
        animator.SetTrigger("Walk");
        yield return StartCoroutine(GoToLocation(destination.transform.position));
        animator.SetTrigger("Idle");
        animator.SetTrigger("Scrub");
           if (actionParticles != null)
            {
                actionParticles.Play(); // Start the ParticleSystem
            }
        yield return new WaitForSeconds(1f);
        item.transform.SetParent(poolOfObjects.transform, false);
        animator.SetTrigger("Idle");
        animator.SetTrigger("Walk");
        yield return StartCoroutine(GoToLocation(originalPosition));
       }
    }
}
