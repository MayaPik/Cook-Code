using UnityEngine;
using TMPro;

public class GetPlayerPoints : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private int playerPoints;

    private void Update()
    {
        // Retrieve the points from PlayerPrefs
        playerPoints = PlayerPrefs.GetInt("PlayerPoints");

        // Update the TextMeshProUGUI component with the player points
        text.text = playerPoints.ToString();
    }
}
