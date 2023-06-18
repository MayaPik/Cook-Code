using System.Collections;
using UnityEngine;

public class TomatoState : MonoBehaviour
{
    [SerializeField] GameObject chef;
    [SerializeField] TomatoStatus tomatoStatus;
    int currentTomatoNumber;
    private float timeToPickTomato = 1f;
    Animator animator;

    private void Awake()
    {
        animator = chef.GetComponent<Animator>();
        tomatoStatus.Tomatoes.Clear();
        tomatoStatus.FullTomatoes.Clear(); 
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                tomatoStatus.Tomatoes.Add(child.gameObject);
                tomatoStatus.FullTomatoes.Add(child.gameObject);
                //TODO: FIX TOMATO STATE;
            }
        }
        currentTomatoNumber = tomatoStatus.Tomatoes.Count;
        StartCoroutine(DecreaseTomatoCount());
    }
    
    private void Update()
    {
        currentTomatoNumber = tomatoStatus.Tomatoes.Count;
    }

    private IEnumerator DecreaseTomatoCount()
    {
        while (tomatoStatus.Tomatoes.Count > 0)
        {
            yield return new WaitForSeconds(timeToPickTomato);
            tomatoStatus.RemoveRandom();

            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(animationLength - timeToPickTomato);
        }
    }
     

    
}
