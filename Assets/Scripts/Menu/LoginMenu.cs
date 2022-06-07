using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class User
{
    public int id;
    public int victory;
    public string username;
    public string password;
    public int played;
}

public class LoginMenu : MonoBehaviour
{
    public TMP_InputField _userText;
    public TMP_InputField _passText;
    public RetainOnLoad Retain;
    private User user = new User();

    IEnumerator GetUser()
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"https://api-heavent.herokuapp.com/users/{_userText.text}/{_passText.text}"))
        {
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                if(www.downloadHandler.text == "null")
                {
                    SceneManager.LoadScene("Login");
                }
                else
                {   
                    string raw = www.downloadHandler.text;
                    user = JsonUtility.FromJson<User>(raw);

                    Retain.setUser(user);
                    user = null;

                    SceneManager.LoadScene("Menu");
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    public void GameLogin() 
    {
        if(_userText.text == "" || _passText.text == "")
                SceneManager.LoadScene("Login");
        else
            StartCoroutine(GetUser());
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
