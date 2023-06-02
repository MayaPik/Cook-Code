using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Charecter : MonoBehaviour
{
    private void OnMouseDown()
    {
    SceneManager.LoadScene(gameObject.name);
    }
}
