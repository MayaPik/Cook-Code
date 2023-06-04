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
        yield return StartCoroutine(GrabItemCoroutine(itemGameObject, hand));
    }
}
