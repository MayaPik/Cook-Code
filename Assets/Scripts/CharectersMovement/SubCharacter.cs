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

    private void Start()
    {
        subPlayerAnimator = GetComponent<Animator>();
        StartCoroutine(ChefAnimationCoroutine());
    }

    private IEnumerator ChefAnimationCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(player.GetItem(tomatoPrefab, subPlayerAnimator, hand));
            yield return null;
        }
    }
}