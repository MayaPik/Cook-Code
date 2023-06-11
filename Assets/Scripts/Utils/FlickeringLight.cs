using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public float duration = 1f;
    private bool isFlickering = true;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (isFlickering)
        {
            // Turn the light on for the specified onDuration
            flickeringLight.enabled = true;

            if (timer >= duration)
            {
                // Switch to the off state after the onDuration
                flickeringLight.enabled = false;
                timer = 0f;
                isFlickering = false;
            }
        }
        else
        {
            // Turn the light off for the specified offDuration
            flickeringLight.enabled = false;

            if (timer >= duration)
            {
                // Switch to the on state after the offDuration
                flickeringLight.enabled = true;
                timer = 0f;
                isFlickering = true;
            }
        }
    }
}
