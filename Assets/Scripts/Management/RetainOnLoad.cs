using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetainOnLoad : MonoBehaviour
{
    // user identity
    private string userName;
    private int userId;

    // current level stats
    private int currentLevelId;
    private int victory; // 0 = win | 1 = loss | 2 = null

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

    public void setUser(string user, int id)
    {
        this.userName = user;
        this.userId = id;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
