using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    public void Playlvl()
    {
        GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().currentLvlData = 
            gameObject.transform.parent.gameObject.GetComponent<entryData>().lvlData;
        SceneManager.LoadScene("LevelLoad");
    }
}
