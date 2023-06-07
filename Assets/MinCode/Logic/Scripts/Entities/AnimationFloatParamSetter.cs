using UnityEngine;
using System.Collections;

public class AnimationFloatParamSetter : MonoBehaviour
{
    public string floatParamName;
    public FloatReference floatValue;
    public Animator animator;

    public void SetSpeed(float value)
    {
        if (animator != null)
        {
            animator.SetFloat(floatParamName, value);
        }
    }

    public void SetSpeed()
    {
        SetSpeed(floatValue);
    }
}
