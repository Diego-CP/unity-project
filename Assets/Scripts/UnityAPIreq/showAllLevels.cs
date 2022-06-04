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
public class LevelsString
{
    public List<Level> levels;
}
public class showAllLevels : MonoBehaviour
{
    public LevelsString Levels;
    public static Level test;

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
        using (UnityWebRequest www = UnityWebRequest.Get("https://api-heavent.herokuapp.com/levels/7"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonString = www.downloadHandler.text;
            
                test = JsonUtility.FromJson<Level>(jsonString);
                GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().lvl = test;
                Debug.Log(test);
            

            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }
    
    public Level rTest()
    {
        return test;
    }


 

}

