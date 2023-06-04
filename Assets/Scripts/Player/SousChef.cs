using System.Collections;
using UnityEngine;

public class SousChef : Player
{

    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        if (itemGameObject.GetComponent<Item>().type == Item.TypeOptions.Object) {
        animator.SetTrigger("Idle");
        animator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(GrabItemCoroutine(itemGameObject, hand));
        }
        else if (itemGameObject.GetComponent<Item>().type == Item.TypeOptions.Abstract) {
            if (itemGameObject.tag == slot.tag) {
            yield return null; 
            } else {
            yield return StartCoroutine(BreakParentLoop());
            }
        }

         else if (itemGameObject.GetComponent<Item>().type == Item.TypeOptions.Destination)
        {
            yield return StartCoroutine(GoToDestination(itemGameObject));
        }
    }
}
