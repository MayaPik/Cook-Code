using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TomatoStatus", menuName = "ScriptableObjects/TomatoStatus")]
public class TomatoStatus : ScriptableObject
{
    public List<GameObject> Tomatoes = new List<GameObject>();
    public List<GameObject> FullTomatoes;
    public int tomatoNumber;

    private void OnEnable()
    {
        FullTomatoes = new List<GameObject>(Tomatoes);
        tomatoNumber = Tomatoes.Count;
    }

    public void RemoveRandom()
    {
        if (Tomatoes.Count == 0)
            return;

        int index = Random.Range(0, Tomatoes.Count);
        GameObject tomato = Tomatoes[index];
        Tomatoes.RemoveAt(index);
        tomato.SetActive(false);
        tomatoNumber = Tomatoes.Count;
    }

    public void RestartTomatoes()
    {
        Tomatoes.Clear();
        Tomatoes.AddRange(FullTomatoes);
        tomatoNumber = Tomatoes.Count;
    }
}
