
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasToGainPoints : MonoBehaviour {
public LevelProgressSaverbyCollision lps;
public int minimumpointstogain;
public GameObject levelfailedpanel;
public void OnCollisionEnter(Collision obj)
{
	
	LevelProgressSaver(obj.gameObject);
}
public void OnCollisionEnter2D(Collision2D obj)
{

	LevelProgressSaver(obj.gameObject);
}
public void OnTriggerEnter2D(Collider2D obj)
{
	
	LevelProgressSaver(obj.gameObject);
}
public void OnTriggerEnter(Collider obj)
{
	
	LevelProgressSaver(obj.gameObject);
}
void LevelProgressSaver(GameObject obj)
{ if(obj.gameObject.tag=="Player")
	{
	
		if(PlayerPrefs.GetInt("PlayerPoints")>=minimumpointstogain)
		{
         lps.UnlockLevel();
		}
			else
	levelfailedpanel.SetActive(true);

	}
}





}
