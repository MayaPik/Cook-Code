using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
private Vector3 vec;
	// Use this for initialization
	void Start () {
		vec=(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	transform.RotateAround(vec, Vector3.up, 200 * Time.deltaTime);
	}
}
