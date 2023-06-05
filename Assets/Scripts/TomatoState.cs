using System.Collections;
using UnityEngine;

public class TomatoState : MonoBehaviour
{
    [SerializeField] public int tomatoNumber = 10;
    private int initialTomatoNumber;

    private void Start()
    {
        initialTomatoNumber = CountActiveTomatoes();
        StartCoroutine(DecreaseTomatoCount());
    }

    private IEnumerator DecreaseTomatoCount()
    {
        while (tomatoNumber > 0)
        {
            yield return new WaitForSeconds(2f);
            
            // Find the last active tomato child
            GameObject lastActiveTomato = FindLastActiveTomato();

            if (lastActiveTomato != null)
            {
                // Deactivate the last active tomato
                lastActiveTomato.SetActive(false);
                tomatoNumber--;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    public void ResetTomatoState()
    {
        // Reset the tomato number to its initial value
        tomatoNumber = initialTomatoNumber;
        
        // Reactivate all existing tomato objects
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private int CountActiveTomatoes()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    private GameObject FindLastActiveTomato()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
