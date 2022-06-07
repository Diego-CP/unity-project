using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetainOnLoad : MonoBehaviour
{
    // user identity
    private string userName;
    private int userId;

    public User usr;

    // current level stats
    public int currentLevelId;
    public int victory; // 0 = win | 1 = loss | 2 = null

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
