using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayLevel : MonoBehaviour
{
    public void Playlvl()
    {
        GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().currentLvlData = 
            gameObject.transform.parent.gameObject.GetComponent<entryData>().lvlData;
        GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().currentLevelId =
            Int32.Parse(gameObject.transform.parent.gameObject.GetComponent<entryData>().lvlID);
        SceneManager.LoadScene("LevelLoad");
    }
}
