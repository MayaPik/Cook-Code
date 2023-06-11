using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorld : MonoBehaviour {
public string Worldnametounlock;
	void Start () {
		if(!PlayerPrefs.HasKey("LevelAvailable"+Worldnametounlock))
		PlayerPrefs.SetInt("LevelAvailable"+Worldnametounlock,1);
	}
}
