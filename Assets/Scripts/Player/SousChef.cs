using System.Collections;
using UnityEngine;

public class SousChef : Player
{
    [SerializeField] TomatoStatus tomatoStatus; 
    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        if (slot.GetComponent<Slot>().type == Slot.TypeOptions.Abstract) {
            if (itemGameObject.tag == slot.tag) {
            yield return null; 
            } else {
            yield return StartCoroutine(BreakParentLoop());
            }
        }
        else if (itemGameObject.GetComponent<Item>().type == Item.TypeOptions.Object) {

        animator.SetTrigger("Idle");
        animator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f);
        GameObject item = Instantiate(itemGameObject);
        Item itemComponent = item.GetComponent<Item>();
        item.transform.localScale = itemComponent.ObjectSize;
        item.transform.localPosition = itemComponent.ObjectLocation;
        item.transform.localRotation = itemComponent.ObjectRotation;
        yield return StartCoroutine(GrabItemCoroutine(item, hand));
        }

        else if (itemGameObject.GetComponent<Item>().type == Item.TypeOptions.Destination)
        {
        yield return new WaitUntil(() => tomatoStatus.tomatoNumber <= 1);
        animator.SetTrigger("Idle");
        animator.SetTrigger("Walk");
        yield return StartCoroutine(GoToDestination(itemGameObject));
        animator.SetTrigger("Idle");
        if (itemGameObject.tag == "chef")
        {
        tomatoStatus.RestartTomatoes();
        }
        }
    }
}
