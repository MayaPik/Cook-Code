using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUpdater : MonoBehaviour
{
    public IntegerVariable passed;
    public IntegerVariable total;
    public Slider progressionSlider;

    private void OnEnable()
    {
        progressionSlider.value = 0f;
    }

    public void OnProgression()
    {
        var progression = Mathf.Clamp((float)passed.GetValue() / total.GetValue(), 0f, 1f);

        progressionSlider.value = progression;
    }
}
