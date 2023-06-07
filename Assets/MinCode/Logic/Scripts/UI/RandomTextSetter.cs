using UnityEngine;
using System.Collections;
using TMPro;

public class RandomTextSetter : MonoBehaviour
{
    public StringReference[] inventory;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        SetRandom();
    }

    public void SetRandom()
    {
        text.text = inventory[Random.Range(0, inventory.Length)];
    }
}
