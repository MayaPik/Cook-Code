using UnityEngine;

public class CameraChanges : MonoBehaviour
{
    private Camera mainCamera;
    private Camera fpsCamera;

   private void Awake()
    {
    mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    fpsCamera = GameObject.Find("FPSCamera").GetComponent<Camera>();
    ChangeToMain();
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
