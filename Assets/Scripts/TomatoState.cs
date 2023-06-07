using System.Collections;
using UnityEngine;

public class TomatoState : MonoBehaviour
{
    public TomatoStatus tomatoStatus;
    public int currentTomatoNumber;
    public GameObject chef;
    Animator animator;

    private void Start()
    {
        animator = chef.GetComponent<Animator>();
        tomatoStatus.Tomatoes.Clear(); // Clear the list before adding active tomatoes
        tomatoStatus.FullTomatoes.Clear();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                tomatoStatus.Tomatoes.Add(child.gameObject);
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
            yield return new WaitForSeconds(1f);
            tomatoStatus.RemoveRandom();

            float animationLength = GetAnimationLength();

            yield return new WaitForSeconds(animationLength - 1f);
        }
    }
     private float GetAnimationLength()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }

    
}
