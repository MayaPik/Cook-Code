using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

	void Update () {
		if(Input.GetKey("d"))
		transform.position=new Vector3(transform.position.x+(5f*Time.deltaTime),transform.position.y,1f);
		if(Input.GetKey("a"))
		transform.position=new Vector3(transform.position.x+(-5f*Time.deltaTime),transform.position.y,1f);
		
	}
	void OnCollisionEnter2D (Collision2D obj)
	{
		if(obj.gameObject.tag=="KillPlayer")
		{
	Destroy(this.gameObject);
		}
	}
	
	
}
