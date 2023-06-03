using System.Collections;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected bool isMain;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public abstract IEnumerator GetItem(GameObject itemGameObject, Animator animator, GameObject hand);

    protected IEnumerator GrabItemCoroutine(GameObject gameObject, GameObject hand)
    {
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

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Bending") == false);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
