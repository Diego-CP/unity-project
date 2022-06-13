using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class lvlStats
{
    public int id;
    public int deaths;
    public int victories;
    public int levelId;
    public int userId;
    public int time;
}
public class LevelStats : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Destroy(gameObject);
        }
    }

    public void getData(int seg, int vic, int deth)
    {
        StartCoroutine(Get(seg,vic,deth));
    }

    IEnumerator Get(int seg, int vic, int deth)
    {
        RetainOnLoad retain =  GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>();
        User usr = retain.usr;
        int lvlid = retain.currentLevelId;
        string send = $"https://api-heavent.herokuapp.com/level_stats/{usr.id}/{lvlid}";
        using (UnityWebRequest www = UnityWebRequest.Get(send))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log("Error: " + www.error);
            else
            {
                string raw = www.downloadHandler.text;
                if(raw == "null")
                {
                    string postJson = "{" + $"\"levelId\":{retain.currentLevelId},\"userId\":{usr.id},\"time\":{seg},\"victories\":{vic},\"deaths\":{deth}" + "}";

                    StartCoroutine(Create(postJson));
                }
                else
                {
                    lvlStats json = JsonUtility.FromJson<lvlStats>(raw);
                    int sendSeg = json.time;
                    int sendVictor = json.victories;
                    int sendDeath = json.deaths;
                    if(json.time > seg)
                        sendSeg = seg;
                    sendVictor += vic;
                    sendDeath += deth;
                    string postJson;
                    string url;
                    if(vic == 1)
                    {
                        postJson = "{" + $"\"time\":{sendSeg},\"victories\":{sendVictor}" + "}";
                        url = $"https://api-heavent.herokuapp.com/level_stats/wins/{usr.id}/{lvlid}";
                    }
              
                    else
                    {
                        postJson = "{" + $"\"deaths\":{sendDeath}" + "}";
                        url = $"https://api-heavent.herokuapp.com/level_stats/deaths/{usr.id}/{lvlid}";
                    }
                    
                    StartCoroutine(Up(postJson,url));

                }
            }
        }

        usr = null;
    }

    IEnumerator Create(string contents)
    {
        User usr = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().usr;
        int lvlid = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().currentLevelId;
        using (UnityWebRequest www = UnityWebRequest.Put($"https://api-heavent.herokuapp.com/level_stats", contents))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log("Error: " + www.error);
        }

        usr = null;
        Destroy(gameObject);
    }

    IEnumerator Up(string contents, string url)
    {
        User usr = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().usr;
        int lvlid = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().currentLevelId;
        Debug.Log(url);
        using (UnityWebRequest www = UnityWebRequest.Put(url, contents))
        {
            www.method = "PUT";
            www.SetRequestHeader("Content-Type", "application/json");
            Debug.Log(contents);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log("Error: " + www.error);
            
        }

        usr = null;
        Destroy(gameObject);
    }


}
