using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCharacter : MonoBehaviour
{
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject poolOfObjects;
    Animator subPlayerAnimator;
    [SerializeField] GameObject tomatoPrefab; 
    [SerializeField] Player player;
    [SerializeField] TomatoStatus tomatoStatus;

    private void Awake()
    {
        table = GameObject.FindGameObjectWithTag("table");
        hand = GameObject.FindGameObjectWithTag("Hand");
        poolOfObjects = GameObject.Find("PoolOfObjects");
    }
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
                Debug.Log("No tomatoes available.");
            }
            yield return null;
        }
    }
}
