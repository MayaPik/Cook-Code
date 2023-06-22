using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class RunFunction : MonoBehaviour
{
    List<Slot> slots;
    [SerializeField] Player player;
    public ButtonReferences buttonReferences;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject FailPanel;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject poolOfObjects;
    [SerializeField] string defaultTrigger;
    private Animator playerAnimator;

    private void Start()
    {
        WinPanel = buttonReferences.WinPanel;
        FailPanel = buttonReferences.FailPanel;
        slots = new List<Slot>();
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger(defaultTrigger);
        CollectChildSlots(transform);

        if (WinPanel != null)
        {
            WinPanel.SetActive(false);
        }
        if (FailPanel != null) 
        {
            FailPanel.SetActive(false);
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
        if (player.GetType() == typeof(Dishwasher))
        {
            player.transform.position = player.originalPosition;
            playerAnimator.SetTrigger("Idle");
        }
        // DestroyPoolOfObjects();
        StartCoroutine(GrabItemsCoroutine());
    }

    private IEnumerator GrabItemsCoroutine()
{
    foreach (Slot slot in slots)
    {
        if (slot.draggedObject == null)
        {
            // Skip to the next slot if draggedObject is null
            continue;
        }
        yield return new WaitForSeconds(0.1f); 
        yield return StartCoroutine(player.GetItem(slot, slot.draggedObject, playerAnimator, hand));

        if (player.shouldBreakLoop)
        {
            break; // Exit the loop if shouldBreakLoop is true
        }
    }

    CheckIfCorrect();
}

private void CheckIfCorrect()
{
    bool isCorrect = true;

    foreach (Slot slot in slots)
    {
        if (slot.draggedObject == null)
        {
            isCorrect = false;
            break;
        }

        string droppedTag = slot.draggedObject.tag;

        if (player.GetType() == typeof(Dishwasher))
        {
            if ((slot.gameObject.GetComponent<PlayerDebugged>().enabled ^ slot.tag == "Dirty"))
            {
                isCorrect = false;
                break;
            }
        }
        else if (slot.tag != droppedTag)
        {
            isCorrect = false;
            break;
        }
    }

    if (!isCorrect)
    {
        TryAgain();
        return;
    }

    if (WinPanel != null)
    {
        WinPanel.SetActive(true);
    }
}



    private void TryAgain()
    {
        if (FailPanel != null)
        {
            // EmptyHands();
            // player.transform.position = player.originalPosition;
            // playerAnimator.SetTrigger("Idle");
            FailPanel.SetActive(true);
            // DestroyPoolOfObjects();
        }
    }
     public void HomeScreen()
    {
        SceneManager.LoadScene("SyncMain");
    }
    

//    private void EmptyHands()
//     {
//     Item itemInHand = hand.GetComponentInChildren<Item>();

//     if (itemInHand != null)
//     {
//         GameObject objectInHand = itemInHand.gameObject;
//         objectInHand.transform.SetParent(poolOfObjects.transform, false);
//     }
//     else
//     {
//         return;
//     }
//     }


//     private void DestroyPoolOfObjects()
//     {
//         if (poolOfObjects.transform.childCount > 0)
//         {
//             int childCount = poolOfObjects.transform.childCount;

//             for (int i = childCount - 1; i >= 0; i--)
//             {
//                 GameObject childObject = poolOfObjects.transform.GetChild(i).gameObject;
//                 Destroy(childObject);
//             }
//         }
//     }
}
