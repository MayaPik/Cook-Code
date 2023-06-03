using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharecterInteractive : MonoBehaviour
{
    private void OnMouseDown()
    {
    SceneManager.LoadScene(gameObject.name);
    }
}
