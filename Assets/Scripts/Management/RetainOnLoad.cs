using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetainOnLoad : MonoBehaviour
{
    public List<Level> lvl;
    private void Awake()
    {
        
        GameObject R = GameObject.Find("Retain");
        if(R != null && R != gameObject)
        {
            Destroy(R);
        }
        
        
    }

    /*
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelSelector")
        {
            
        }
    }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
