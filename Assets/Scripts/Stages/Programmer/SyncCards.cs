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
    public ButtonReferences buttonReferences;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject PracticePanel;
    [SerializeField] GameObject[] ToolsForExplain;
    [SerializeField] GameObject[] ToolsForPractice;
    private string sceneMode = "SyncExplained";
    public LevelTextManager levelTextManager;


    private void Start()
    {
        currentIndex = 0;
        string sceneName = SceneManager.GetActiveScene().name;
        WinPanel = buttonReferences.WinPanel;
        PracticePanel = buttonReferences.PracticePanel;
        childAnimators = new List<Animator>();
        buttons = new List<Button>();
        CollectChildAnimators(transform);
        CollectButtons(transform);
        levelTextManager = FindObjectOfType<LevelTextManager>();

        foreach (Animator animator in childAnimators)
        {
            animator.enabled = false;
        }

        if (PracticePanel != null)
        {
            PracticePanel.SetActive(false);
        }
        if (WinPanel != null)
        {
            WinPanel.SetActive(false);
        }

    StartCoroutine(StartAnimationSequence(sceneName));
}

    private IEnumerator StartAnimationSequence(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        while (!levelTextManager.isPopupClosed)
        {
            yield return null;
        }

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
                PracticePanel.SetActive(true);
            }
            else if (sceneMode == "SyncPractice")
            {
                WinPanel.SetActive(true);
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
                
                StartCoroutine(WaitForAnimationFinish(animator));
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator WaitForAnimationFinish(Animator animator)
    {
        GameObject ActiveTool = (sceneMode == "SyncExplained") ? ToolsForExplain[currentIndex] : ToolsForPractice[currentIndex];
        ActiveTool.SetActive(true);

        // Wait until the current animation is finished
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
           
            yield return null;
        }

        // Disable the current animator and move to the next one
        animator.enabled = false;
        currentIndex++;
        ActiveTool.SetActive(false);

        // Play the next animation
        PlayNextAnimation(SceneManager.GetActiveScene().name);
    }

    public void ChangeScene(string name)
    {
        sceneMode = name;
        Start();
    }
}
