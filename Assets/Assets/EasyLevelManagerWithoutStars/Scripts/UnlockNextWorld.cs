using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNextWorld : MonoBehaviour {
public GameObject LevelPassedPanel;	
public int CurrentLevel;
public int totalstarstounlocknextworld;
public string WorldName;
public string NextWorldName;
private int points;
private int temppoints;
private int totalstars=0;
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
{ 
if(obj.gameObject.tag=="Player")
	UnlockLevel(obj);
	
}
public void UnlockLevel(GameObject obj)
{
	
	 
	if(obj.gameObject.tag=="Player")
	{   
        temppoints=PlayerPrefs.GetInt((CurrentLevel-1).ToString()+WorldName+"temp");
		
		points=PlayerPrefs.GetInt((CurrentLevel-1).ToString()+WorldName);
	
		if(points<temppoints)
		PlayerPrefs.SetInt((CurrentLevel-1).ToString()+WorldName,temppoints);
	
		LevelPassedPanel.SetActive(true);
		if(PlayerPrefs.GetInt("LevelAvailable"+WorldName)>CurrentLevel)
		{

		}
		else{
		PlayerPrefs.SetInt("LevelAvailable"+WorldName,CurrentLevel);
		}
		totalstars=0;
		for(int i=0;i< PlayerPrefs.GetInt("LevelAvailable"+WorldName);i++)
        totalstars=totalstars+PlayerPrefs.GetInt(i.ToString()+WorldName);
		Debug.Log(totalstars);
		if(totalstars>=totalstarstounlocknextworld&&!PlayerPrefs.HasKey("LevelAvailable"+NextWorldName))
		PlayerPrefs.SetInt("LevelAvailable"+NextWorldName,1);
     
	}
}
}
