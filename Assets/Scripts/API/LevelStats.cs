using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class LevelStats : MonoBehaviour
{
    private LvlStats lvlStats = new LvlStats();

    public void addData()
    {
        StartCoroutine(Post());
    }

    IEnumerator Post()
    {
        RetainOnLoad retain = GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>();

        using (UnityWebRequest www = UnityWebRequest.Get($"https://api-heavent.herokuapp.com/level_stats/{retain.userId}/{retain.currentLevelId}"))
        {
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                if(www.downloadHandler.text != "null")
                {   
                    string rawGet = www.downloadHandler.text;
                    lvlStats = JsonUtility.FromJson<LvlStats>(rawGet);

                    if(retain.victory == 0)
                    {
                        lvlStats.victories += 1;
                    }
                    else if(retain.victory == 1)
                    {
                        lvlStats.deaths += 1;
                    }
                    
                    if((retain.finalTime - retain.initialTime) < lvlStats.time)
                        lvlStats.time = (retain.finalTime - retain.initialTime);

                    string rawPut = "{" + $"\"deaths\":{lvlStats.deaths},\"time\":{lvlStats.time.Minutes},\"victories\":{lvlStats.victories}" + "}";

                    using (UnityWebRequest ww = UnityWebRequest.Put($"https://api-heavent.herokuapp.com/level_stats/{retain.userId}/{retain.currentLevelId}", rawPut))
                    {
                        ww.method = "PUT";
                        ww.SetRequestHeader("Content-Type", "application/json");
                        yield return ww.SendWebRequest();

                        if (www.result != UnityWebRequest.Result.Success)
                            Debug.Log("Error: " + ww.error);            
                    }

                    lvlStats = null;
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }
}

public class LvlStats
{
    public int id;
    public int deaths;
    public int victories;
    public int userId;
    public int levelId;
    public TimeSpan time;
}
