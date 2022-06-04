using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    public int id;
    public string levelData;
    public int totalVictories;
    public int totalBosses;
    public int dislikes;
    public string name;
    public int userId;
    public int totalDeaths;
    public int totalEnemies;
    public int likes;
    
}

public class showAllLevels : MonoBehaviour
{
    public List<Level> Levels;
    public static Level test;
    private string constring = "https://api-heavent.herokuapp.com/levels";


    private void Start()
    {
        LevelLoadRequest();
    }

    public void LevelLoadRequest()
    {
        StartCoroutine(GetLevels());
    }
    public IEnumerator GetLevels()
    {

        using (UnityWebRequest www = UnityWebRequest.Get(constring))
        {
            Levels = new List<Level>();
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {

                string raw = www.downloadHandler.text;
                string elements = raw.Substring(1, raw.Length - 2);
                string[] prefilter = elements.Split("\"dislikes\":");
                int dislik;
                for (int i = 1; i < prefilter.Length; i++)
                {
                    dislik =  (int)char.GetNumericValue((prefilter[i][0]));
                    if(i < prefilter.Length-1)
                        prefilter[i] = prefilter[i].Substring(3);  
                    prefilter[i - 1] = prefilter[i - 1] + "\"dislikes\": " + dislik + "}";
                    //prefilter[i].Replace((char)39, (char)34);
                    Debug.Log(prefilter[i-1]);
                    Level yes = JsonUtility.FromJson<Level>(prefilter[i - 1]);
                    Levels.Add(yes);
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
        GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().lvl = Levels;

    }
    
    public Level rTest()
    {
        return test;
    }


 

}

