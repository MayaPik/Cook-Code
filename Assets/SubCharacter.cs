using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCharacter : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject tomatoPrefab; 
    [SerializeField] GameObject hand;
    [SerializeField] GameObject poolOfObjects;
    Animator subPlayerAnimator;

    private void Start()
    {
        subPlayerAnimator = GetComponent<Animator>();
        StartCoroutine(ChefAnimationCoroutine());
    }

    private IEnumerator ChefAnimationCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(GrabItemCoroutine(tomatoPrefab, subPlayerAnimator));
            yield return null;
        }
    }

    private IEnumerator GrabItemCoroutine(GameObject gameObject, Animator subPlayerAnimator)
    {
        subPlayerAnimator.SetTrigger("Idle");
        subPlayerAnimator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f);
        GameObject item = Instantiate(gameObject);
        item.transform.localScale = gameObject.GetComponent<Item>().objectSize;
        item.transform.localPosition = gameObject.GetComponent<Item>().objectLocation;
        item.transform.localRotation = gameObject.GetComponent<Item>().objectRotation;
        CapsuleCollider handCollider = hand.GetComponent<CapsuleCollider>();
        if (handCollider != null)
        {
            item.transform.SetParent(handCollider.transform, false);
        }
        yield return new WaitForSeconds(2f);

        ObjectAction objectAction = item.GetComponent<ObjectAction>();
        if (objectAction != null)
        {
            objectAction.PerformAction();
        }

        yield return new WaitUntil(() => subPlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Bending") == false);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
