using System.Collections;
using UnityEngine;

public class Chef : Player
{
    public override IEnumerator GetItem(GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(GameObject itemGameObject, Animator animator, GameObject hand)
    {
        animator.SetTrigger("Idle");
        animator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(GrabItemCoroutine(itemGameObject, hand));
    }
}
