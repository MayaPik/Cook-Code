using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPoints : MonoBehaviour {

    [SerializeField] int pointstoset;

    public void SetPoints()
        {
        int playerPoints = PlayerPrefs.GetInt("PlayerPoints");
    
        PlayerPrefs.SetInt("PlayerPoints",playerPoints+pointstoset);
        }
	
}
