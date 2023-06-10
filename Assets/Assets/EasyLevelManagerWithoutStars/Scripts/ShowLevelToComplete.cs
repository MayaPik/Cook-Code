using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowLevelToComplete : MonoBehaviour {
public Text text;
public EasyLevelManagerWithoutStars elm;
public void ShowLevelToCOMPLETE(int levelnumber)
{
if(PlayerPrefs.HasKey("LevelAvailable"+elm.Worldname))	
text.text="Play level "+levelnumber+ " to unlock level "+(levelnumber+1);
else
text.text="Complete previous world first";
StartCoroutine(Show());
}
IEnumerator Show()
{
    yield return new WaitForSeconds(3f);
    text.text="";
    }
}
