using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanges : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera fpsCamera;

    void Start() {
        ChangeToMain();
    }
   public void ChangeaToFps() {
    fpsCamera.enabled = true;
    fpsCamera.GetComponentInChildren<MeshRenderer>().enabled = true;
    mainCamera.enabled = false;
   }
   public void ChangeToMain() {
    mainCamera.enabled = true;
    fpsCamera.enabled = false;
    fpsCamera.GetComponentInChildren<MeshRenderer>().enabled = false;

    
   }
}
