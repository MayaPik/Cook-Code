using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCharacter : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject tomatoPrefab; 
    [SerializeField] GameObject hand;
    [SerializeField] GameObject poolOfObjects;
    Animator subPlayerAnimator;
    [SerializeField] Player player;
    [SerializeField] TomatoStatus tomatoStatus;

    private void Start()
    {
        subPlayerAnimator = GetComponent<Animator>();
        StartCoroutine(ChefAnimationCoroutine());
    }

    private IEnumerator ChefAnimationCoroutine()
    {
        while (true)
        {
            if (tomatoStatus.tomatoNumber > 0)
            {
                yield return StartCoroutine(player.GetItem(null, tomatoPrefab, subPlayerAnimator, hand));
            }
            else
            {
                // Handle case when there are no tomatoes available
                Debug.Log("No tomatoes available.");
            }
            yield return null;
        }
    }
}
