using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void ToMenu()
    {

        
        SceneManager.LoadScene("LevelLoad");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
