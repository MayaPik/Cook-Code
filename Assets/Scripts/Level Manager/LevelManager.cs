using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string Worldname;
    [SerializeField] string NextWorldname;
    [SerializeField] GameObject[] Levellockbuttons;
    [SerializeField] GameObject spotlight;
    private int totalstars = 0;
    private bool keycheck;

    void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("LevelAvailable" + Worldname); i++)
        {
            totalstars = totalstars + PlayerPrefs.GetInt(i.ToString() + Worldname);
        }

        for (int i = 0; i < Levellockbuttons.Length; i++)
        {
            Levellockbuttons[i].transform.parent.GetComponent<MouseDownButton>().locked = true;
            Levellockbuttons[i].SetActive(true);
        }
    }

    void Update()
    {
        keycheck = PlayerPrefs.HasKey("LevelAvailable" + Worldname);
        if (keycheck)
        {
            int levelAvailable = PlayerPrefs.GetInt("LevelAvailable" + Worldname);
            for (int i = 0; i < levelAvailable; i++)
            {
                Levellockbuttons[i].transform.parent.GetComponent<MouseDownButton>().locked = false;
                Levellockbuttons[i].SetActive(false);
				spotlight.transform.position = Levellockbuttons[i].transform.parent.position;
            }

            if (levelAvailable == Levellockbuttons.Length)
            {
                PlayerPrefs.SetInt("LevelAvailable" + NextWorldname, 1);
            }
        }
    }
}
