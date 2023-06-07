using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DecreasingSliderController : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public UnityEvent onFull;
    public FloatReference decreaseRate;
    public FloatReference increaseRate;
    public FloatVariable mixSpeed;

    private void OnEnable()
    {
        slider.value = 0;
    }

    private void FixedUpdate()
    {
        if (slider.value != 1)
        {
            UpdateSliderValue(-decreaseRate * Time.fixedDeltaTime);
        }
    }

    public void Increase()
    {
        UpdateSliderValue(increaseRate);
    }

    private void UpdateSliderValue(float delta)
    {
        slider.value = Mathf.Clamp(slider.value + delta, 0f, 1f);
        fill.color = gradient.Evaluate(slider.value);
        mixSpeed.SetValue(slider.value);

        if (slider.value == 1)
        {
            onFull.Invoke();
        }
    }
}
