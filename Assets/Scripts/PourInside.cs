using UnityEngine;

public class PourInside : ObjectAction
{
    [SerializeField] GameObject poolOfObjects;
    [SerializeField] GameObject player;
        Animator playerAnimator;



    public override void PerformAction()
    {

        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Pour");
        transform.SetParent(poolOfObjects.transform, false);
        gameObject.SetActive(false);
        
    }
}
