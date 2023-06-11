using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPoints : MonoBehaviour {

public void SetPoints(int pointstoset)
{
PlayerPrefs.SetInt("PlayerPoints",pointstoset);
}
	
}
