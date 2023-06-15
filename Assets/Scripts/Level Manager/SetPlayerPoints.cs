using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPoints : MonoBehaviour {

    [SerializeField] int pointstoset;
    private int playerPoints;

    public void SetPoints()
        {
        playerPoints = PlayerPrefs.GetInt("PlayerPoints");
        PlayerPrefs.SetInt("PlayerPoints",playerPoints+pointstoset);
        
        }
	
}
