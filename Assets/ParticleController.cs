using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public GameObject particles;
    public Camera activeCamera;
    private Vector3 mousePos;

    public void Start() {
        particles.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartParticleSystem();
        }
    }

    public void StartParticleSystem()
    {
        mousePos = activeCamera.ScreenToWorldPoint(Input.mousePosition);
        particles.SetActive(true);
        particles.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
    }
}
