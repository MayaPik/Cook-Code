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

        for (int i = 0; i < timesToRepeat; i++)
        {
            yield return StartCoroutine(BakingCoroutine(animator,itemGameObject, hand));
        }

        // All coroutines are done
    }

    private IEnumerator BakingCoroutine(Animator animator, GameObject itemGameObject, GameObject hand)
    {
        animator.SetTrigger("Roll");
        GameObject item = itemGameObject;
        Item itemComponent = item.GetComponent<Item>();
        item.transform.localScale = itemComponent.ObjectSize;
        item.transform.localPosition = itemComponent.ObjectLocation;
        item.transform.localRotation = itemComponent.ObjectRotation;
        yield return StartCoroutine(GrabItemCoroutine(item, hand));
           if (actionParticles != null)
            {
                actionParticles.Play(); // Start the ParticleSystem
            }
        yield return new WaitForSeconds(1.0f);
    }
}
