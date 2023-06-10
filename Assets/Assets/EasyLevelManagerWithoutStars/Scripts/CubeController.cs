using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {
public Transform Cube;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("w"))
		{
          Cube.position += Vector3.forward * Time.deltaTime * 5;
		}
		if(Input.GetKey("s"))
		{
          Cube.position += Vector3.back * Time.deltaTime*5;
		}
		if(Input.GetKey("a"))
		{
          Cube.position += Vector3.left * Time.deltaTime*5;
		}
		if(Input.GetKey("d"))
		{
          Cube.position += Vector3.right * Time.deltaTime*5;
		}
	}
}
