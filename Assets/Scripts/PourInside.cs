using System.Collections;
using UnityEngine;

public class PourInside : ObjectAction
{
    [SerializeField] private GameObject poolOfObjects;
    [SerializeField] private GameObject player;
    private Animator playerAnimator;

    public override void PerformAction()
    {
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Pour");
        StartCoroutine(WaitForPouringAnimation());
    }

    private IEnumerator WaitForPouringAnimation()
    {
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pouring"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        transform.SetParent(poolOfObjects.transform, false);
        gameObject.SetActive(false);
            }
}
