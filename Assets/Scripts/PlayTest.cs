using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTest : MonoBehaviour
{

    public static bool pressEdit = true;
    public static bool pressPlay = false;
    public GameObject EditorUI;
    // Update is called once per frame
    void Update()
    {
        if (pressEdit)
            Edit();
        if (pressPlay)
            Play();
    }

    public void EditClick()
    {
        pressEdit = true;
        pressPlay = false;
    }
    public void PlayClick()
    {
        pressPlay = true;
        pressEdit = false;
    }
    public void Play()
    {
        EditorUI.SetActive(false);
        Time.timeScale = 1f;
        
    }
    public void Edit()
    {
        EditorUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

    
}
