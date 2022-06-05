using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetainOnLoad : MonoBehaviour
{
    public List<Level> lvl;
    public string currentLvlData;
    private void Awake()
    {
        
        GameObject R = GameObject.Find("Retain");
        if(R != null && R != gameObject)
        {
            Destroy(R);
        }
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
