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
        SetItemProperties(item, itemComponent);
    
        yield return StartCoroutine(GrabItemCoroutine(item, hand));
    }
}
