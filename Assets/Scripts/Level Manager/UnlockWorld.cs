using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorld : MonoBehaviour {

    [SerializeField] private string Worldnametounlock;
	void Start () {
			if(!PlayerPrefs.HasKey("LevelAvailable"+Worldnametounlock))
			PlayerPrefs.SetInt("LevelAvailable"+Worldnametounlock,1);
	}
}
