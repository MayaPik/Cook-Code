using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class RunFunction : MonoBehaviour
{
    [SerializeField] List<Slot> slots;
    [SerializeField] Player player;
    [SerializeField] GameObject buttonHome;
    [SerializeField] GameObject buttonAgain;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject poolOfObjects;
    [SerializeField] string defaultTrigger;
    private Animator playerAnimator;

    private void Start()
    {
        slots = new List<Slot>();
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger(defaultTrigger);
        CollectChildSlots(transform);

        if (buttonHome != null)
        {
            buttonHome.SetActive(false);
        }
    }

    private void CollectChildSlots(Transform parent)
    {
        Slot slot = parent.GetComponent<Slot>();
        if (slot != null)
        {
            slots.Add(slot);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            CollectChildSlots(child);
        }
    }

    public void RunInput()
    {
        player.shouldBreakLoop = false;
        if (slots.Count == 0)
        {
            return;
        }
        DestroyPoolOfObjects();
        StartCoroutine(GrabItemsCoroutine());
    }

    private IEnumerator GrabItemsCoroutine()
    {
        foreach (Slot slot in slots)
        {
            if (slot.draggedObject != null)
            {
                yield return StartCoroutine(player.GetItem(slot, slot.draggedObject, playerAnimator, hand));
            }
             if (player.shouldBreakLoop)
        {
            break; // Exit the loop if shouldBreakLoop is true
        }
        }

        CheckIfCorrect();
    }

    private void CheckIfCorrect()
    {
        foreach (Slot slot in slots)
        {
            if (slot.draggedObject == null)
            {
                TryAgain();
                return;
            }
            else
            {
                string droppedTag = slot.draggedObject.tag;
                if (slot.tag != droppedTag)
                {
                    TryAgain();
                    return;
                }
            }
        }

        if (buttonHome != null)
        {
            buttonHome.SetActive(true);
        }
    }

    private void TryAgain()
    {
        if (buttonAgain != null)
        {
            playerAnimator.SetTrigger("Idle");
            buttonAgain.SetActive(true);
            DestroyPoolOfObjects();
        }
    }

    private void DestroyPoolOfObjects()
    {
        if (poolOfObjects.transform.childCount > 0)
        {
            int childCount = poolOfObjects.transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject childObject = poolOfObjects.transform.GetChild(i).gameObject;
                Destroy(childObject);
            }
        }
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("SyncMain");
    }
}
