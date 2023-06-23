using System.Collections;
using UnityEngine;

public class Baker : Player
{
    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        int timesToRepeat = int.Parse(itemGameObject.tag);
        animator.SetTrigger("Idle");
        if (timesToRepeat == 0) 
        {
            yield return StartCoroutine(BreakParentLoop());

        }
        else {
        for (int i = 0; i < timesToRepeat; i++)
        {
            yield return StartCoroutine(BakingCoroutine(animator,itemGameObject, hand));
        }
        }
    }

    private IEnumerator BakingCoroutine(Animator animator, GameObject itemGameObject, GameObject hand)
    {
        animator.SetTrigger("Roll");
        GameObject item = itemGameObject;
        Item itemComponent = item.GetComponent<Item>();
        SetItemProperties(item, itemComponent);
        yield return StartCoroutine(GrabItemCoroutine(item, hand));
           if (actionParticles != null)
            {
                actionParticles.Play(); // Start the ParticleSystem
            }
        yield return new WaitForSeconds(1.0f);
    }
}
