using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DelayBeforeEnabled : MonoBehaviour
{
    public GameObject toDelay;
    public FloatReference delaySeconds;

    private void Awake()
    {
        toDelay.SetActive(false);
    }

    private void OnEnable()
    {
        toDelay.SetActive(false);
        StartCoroutine(WaitThenActivate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaitThenActivate()
    {
        yield return new WaitForSecondsRealtime(delaySeconds);
        toDelay.SetActive(true);
    }
}
