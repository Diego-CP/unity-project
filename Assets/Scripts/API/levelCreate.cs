using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class levelCreate : MonoBehaviour {
 
    public TMP_InputField _name;
    public GameObject Naming;
    string _levelDoc;
    private RetainOnLoad retain;

    private void Update() {
        if(retain == null && GameObject.Find("Retain") != null)
            retain = GameObject.Find("Retain").GetComponent<RetainOnLoad>();
    }    
    
    public void ActivateNaming()
    {
        Level_Manager lvManager = GameObject.Find("LevelManager").gameObject.GetComponent<Level_Manager>();
        lvManager.Savelevel();
        Naming.SetActive(true);
    }

    public void GetLevel()
    {
        Level_Manager lvManager = GameObject.Find("LevelManager").gameObject.GetComponent<Level_Manager>();
        _levelDoc = lvManager.GetJson().Replace((char)34, (char)39).Replace("\n", "");
        Login();
    }

    public void DeActivateNaming()
    {
        GetLevel();
        Naming.SetActive(false);
    }

    public void CloseTab()
    {
        Naming.SetActive(false);
    }

    public void Login()
    {
        StartCoroutine(Post());
    }
 
    IEnumerator Post()
    {
        var user = new UserLevel
        {
            userId = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().usr.id.ToString(),
            name = _name.text,
            levelData = _levelDoc,
        };
 
        var body = JsonUtility.ToJson(user);
        Debug.Log(body);
        
        using (UnityWebRequest www = UnityWebRequest.Put("https://api-heavent.herokuapp.com/levels", body))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
        }

        user = null;

    }
}

public class UserLevel
{
    public string userId;
    public string name;
    public string levelData;
}