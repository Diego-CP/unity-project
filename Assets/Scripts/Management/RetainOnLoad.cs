using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class RetainOnLoad : MonoBehaviour
{
    // user identity
    private string userName;
    public int userId;

    public User usr;

    // current level stats
    public int currentLevelId;
    public int victory; // 0 = victory | 1 = loss | 2 = null
    public DateTime initialTime;
    public DateTime finalTime;

    // level loading
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

    public void setUser(User user)
    {
       usr = user;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
