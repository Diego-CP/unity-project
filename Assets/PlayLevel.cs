using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayLevel : MonoBehaviour
{
    public void Playlvl()
    {
        RetainOnLoad retain = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>();
        retain.currentLvlData = 
            gameObject.transform.parent.gameObject.GetComponent<entryData>().lvlData;
        retain.currentLevelId =
            Int32.Parse(gameObject.transform.parent.gameObject.GetComponent<entryData>().lvlID);
        SceneManager.LoadScene("LevelLoad");
        retain.initialTime = DateTime.Now;
    }
}
