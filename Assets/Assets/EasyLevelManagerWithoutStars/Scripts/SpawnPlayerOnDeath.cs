using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnDeath : MonoBehaviour {
public GameObject pl;
private bool spawn=true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(GameObject.FindGameObjectWithTag("Player")==null&&spawn)
		StartCoroutine(Wait());
	}
	IEnumerator Wait()
	{
		spawn=false;
		yield return new WaitForSeconds(1f);
		Instantiate(pl,pl.transform.position,Quaternion.identity);
		spawn=true;
	}
}
