using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SyncCards : MonoBehaviour
{
    private List<Animator> childAnimators;
    private int currentIndex = 0;
    private GameObject canvasNext;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        childAnimators = new List<Animator>();
        CollectChildAnimators(transform);

        // Disable all child animators initially
        foreach (Animator animator in childAnimators)
        {
            animator.enabled = false;
        }

       
        canvasNext = GameObject.FindGameObjectWithTag("ButtonNext");

        if (canvasNext != null)
        {
            canvasNext.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No GameObject found with tag: ButtonNext");
        }

        // Start playing the animations
        PlayNextAnimation(sceneName);
    }

    private void CollectChildAnimators(Transform parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        if (animator != null)
        {
            childAnimators.Add(animator);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            CollectChildAnimators(child);
        }
    }

    private void PlayNextAnimation(string sceneName)
    {
        // Check if all animations have been played
        if (currentIndex >= childAnimators.Count)
        {
            Debug.Log("All animations played.");
            canvasNext.SetActive(true);
            return;
        }

        Animator currentAnimator = childAnimators[currentIndex];
        currentAnimator.enabled = true;
        //TODO: FIX THE BUTTONS SHOULD BE DISABLED WHEN ITS NOT THEIT TURN;

        // Enable and play the next animation
        if (sceneName == "SyncExplained")
        {
            currentAnimator.SetTrigger("MoveManual");
            StartCoroutine(WaitForAnimationFinish(currentAnimator));
        }
        else if (sceneName == "SyncPractice")
        {
            StartCoroutine(CheckAnimationState(currentAnimator));
        }
    }

    private IEnumerator CheckAnimationState(Animator animator)
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);


            if (stateInfo.IsTag("Movement"))
            {
                Debug.Log("Move");
                StartCoroutine(WaitForAnimationFinish(animator));
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator WaitForAnimationFinish(Animator animator)
    {
        // Wait until the current animation is finished
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        // Disable the current animator and move to the next one
        animator.enabled = false;
        currentIndex++;

        // Play the next animation
        PlayNextAnimation(SceneManager.GetActiveScene().name);
    }
}
