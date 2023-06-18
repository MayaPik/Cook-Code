using System.Collections;
using UnityEngine;

public class Chef : Player
{
    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
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
}
