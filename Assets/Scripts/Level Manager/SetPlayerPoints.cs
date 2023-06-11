using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPoints : MonoBehaviour {
    
    public int pointstoset;

    public void SetPoints()
        {
        PlayerPrefs.SetInt("PlayerPoints",pointstoset);
        }
	
}
