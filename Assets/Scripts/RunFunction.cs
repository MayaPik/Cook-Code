using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunFunction : MonoBehaviour
{
    [SerializeField] List<Slot> slots;
    [SerializeField] GameObject player;
    [SerializeField] GameObject subPlayer;
    [SerializeField] GameObject buttonHome;
    [SerializeField] GameObject buttonAgain;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject poolOfObjects;
    [SerializeField] GameObject table;

    Animator playerAnimator;


    private void Start()
    {
        slots = new List<Slot>();
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Think");
        
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
                Debug.Log(slot.draggedObject.name);
                yield return StartCoroutine(GrabItemCoroutine(slot.draggedObject));
            }
        }

        CheckIfCorrect();
    }

    private IEnumerator GrabItemCoroutine(GameObject gameObject)
    {
        playerAnimator.SetTrigger("Idle");

        playerAnimator.SetTrigger("Bend");
        yield return new WaitForSeconds(2f); 
        GameObject item = Instantiate(gameObject);
        item.transform.localScale = gameObject.GetComponent<Item>().objectSize;
        item.transform.localPosition = gameObject.GetComponent<Item>().objectLocation;
        item.transform.localRotation = gameObject.GetComponent<Item>().objectRotation;
        CapsuleCollider handCollider = hand.GetComponent<CapsuleCollider>();
        Debug.Log(handCollider);
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

        yield return new WaitUntil(() => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Bending") == false);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
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

    void DestroyPoolOfObjects() {
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
