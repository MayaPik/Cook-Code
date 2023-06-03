using System.Collections;
using UnityEngine;

public class TomatoState : MonoBehaviour
{
    [SerializeField] private int tomatoNumber = 10;

    private void Start()
    {
        StartCoroutine(DecreaseTomatoCount());
    }

    private IEnumerator DecreaseTomatoCount()
    {
        while (tomatoNumber > 0)
        {
            yield return new WaitForSeconds(2f); 
            Transform lastChild = gameObject.transform.GetChild(gameObject.transform.childCount - 1);
            Destroy(lastChild.gameObject);

            tomatoNumber--;
            yield return new WaitForSeconds(3f); 
        }
    }
}
