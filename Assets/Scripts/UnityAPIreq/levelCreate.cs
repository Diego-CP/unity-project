using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class levelCreate : MonoBehaviour {
 
    public TMP_InputField _username;
    string _levelDoc;
    
    
    
    public void GetLevel()
    {
        Level_Manager lvManager = GameObject.Find("LevelManager").gameObject.GetComponent<Level_Manager>();
        _levelDoc = lvManager.GetJson().Replace((char)34, (char)39).Replace(Environment.NewLine, " ");
        Login();

    }
    public void Login()
    {
        StartCoroutine(Post());
    }
 
    IEnumerator Post()
    {
        var user = new UserLevel
        {
            id = Int32.Parse(_username.text),
            userId = 100,
            name = "Test",
            levelData = _levelDoc,
          
        };
 
        var body = JsonUtility.ToJson(user);
        Debug.Log(body);
        UnityWebRequest request = UnityWebRequest.Post("https://api-heavent.herokuapp.com/levels", body);
        request.SetRequestHeader("content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
 
        yield return request.SendWebRequest();
 
        if (request.isNetworkError || request.isHttpError) Debug.Log(request.error);
 
        Debug.Log($"Response as byte: {request.downloadHandler.data}");
        Debug.Log($"Response as string: {request.downloadHandler.text}");
    }
}


public class UserLevel
{
    public int id;
    public int userId;
    public string name;
    public string levelData;
    int totalDeaths;
    int totalVictories;
    int totalEnemies;
    int totalBosses;
    int totalLikes;
    int totalDislikes;
}