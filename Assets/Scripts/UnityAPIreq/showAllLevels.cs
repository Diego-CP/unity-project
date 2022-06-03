using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Level{
    public int id;
    public string name;
    public string levelData;
    public int userId;
    public int totalDeaths;
    public int totalVictories;
    public int totalBosses;
    public int totalEnemies;
    public int likes;
    public int dislikes;
}
/*
public class ShowAllLevels : MonoBehaviour
{
    
    public List<Level> levels = new List<Level>();

    void Start()
    {
        StartCoroutine(GetLevels());
    }

    IEnumerator GetLevels()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api-heavent.herokuapp.com/levels"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                Debug.Log("Levels: " + www.downloadHandler.text);
                levels =  JsonHelper.getJsonArray<Level>(www.downloadHandler.text);
            }
        }
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
*/