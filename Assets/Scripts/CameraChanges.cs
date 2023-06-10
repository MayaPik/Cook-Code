using UnityEngine;

public class CameraChanges : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera fpsCamera;

    private void Start()
    {
        ChangeCamera(mainCamera);
    }

    public void ChangeToMain()
    {
        ChangeCamera(mainCamera);
    }

    public void ChangeToFps()
    {
        ChangeCamera(fpsCamera);
    }

    private void ChangeCamera(Camera cameraToEnable)
    {
        mainCamera.enabled = false;
        fpsCamera.enabled = false;
        fpsCamera.GetComponentInChildren<MeshRenderer>().enabled = false;

        cameraToEnable.enabled = true;
        if (cameraToEnable == fpsCamera)
        {
            fpsCamera.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
}
