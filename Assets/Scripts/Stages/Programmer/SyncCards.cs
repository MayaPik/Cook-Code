using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SyncCards : MonoBehaviour
{
    private List<Animator> childAnimators;
    private List<Button> buttons;

    private int currentIndex = 0;
    public GameObject practiceButton;
    public GameObject nextScreenButton;
    public string sceneMode = "SyncExplained";

    private void Start()
    {
        currentIndex = 0;
        string sceneName = SceneManager.GetActiveScene().name;
        childAnimators = new List<Animator>();
        buttons = new List<Button>();
        CollectChildAnimators(transform);
        CollectButtons(transform);
        
        foreach (Animator animator in childAnimators)
        {
            animator.enabled = false;
        }

        if (practiceButton != null)
        {
            practiceButton.SetActive(false);
        }
        if (nextScreenButton != null)
        {
            nextScreenButton.SetActive(false);
        }

        // Start playing the animations
        PlayNextAnimation(sceneName);
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Button button = buttons[i];
            button.interactable = (i == currentIndex);
        
        }
    }

    private void CollectChildAnimators(Transform parent)
    {
        Animator animator = parent.GetComponent<Animator>();

        if (animator != null && parent.parent != null && parent.parent.parent != null && parent.parent.gameObject.activeInHierarchy)
        {
            childAnimators.Add(animator);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            CollectChildAnimators(child);
        }
    }

    private void CollectButtons(Transform parent)
    {
        Button button = parent.GetComponent<Button>();

        if (button != null && parent.parent != null && parent.parent.parent != null && parent.parent.gameObject.activeInHierarchy)
        {
            buttons.Add(button);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            CollectButtons(child);
        }
    }

    private void PlayNextAnimation(string sceneName)
    {
        // Check if all animations have been played
        if (currentIndex >= childAnimators.Count)
        {
            Debug.Log("All animations played.");

            if (sceneMode == "SyncExplained")
            {
                practiceButton.SetActive(true);
            }
            else if (sceneMode == "SyncPractice")
            {
                nextScreenButton.SetActive(true);
            }

            return;
        }

        Animator currentAnimator = childAnimators[currentIndex];
        currentAnimator.enabled = true;

        // Enable and play the next animation
        if (sceneMode == "SyncExplained")
        {
            currentAnimator.SetTrigger("MoveManual");
            StartCoroutine(WaitForAnimationFinish(currentAnimator));
        }
        else if (sceneMode == "SyncPractice")
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

    public void ChangeScene(string name)
    {
        sceneMode = name;
        Start();
    }
}
