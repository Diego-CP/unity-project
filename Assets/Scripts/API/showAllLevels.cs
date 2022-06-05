using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    bool load;
    public GameObject levelEntryItem;
    public Transform scroll;


    private void Start()
    {
        load = true;
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
        if (load)
            dislpayLevels(Levels) ;
        load = false;


    }

    public void dislpayLevels(List<Level> levels)
    {
       
        
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject displayItem = Instantiate(levelEntryItem, transform.position, Quaternion.identity);

            displayItem.transform.SetParent(scroll);
            displayItem.GetComponent<entryData>().lvlName = levels[i].name;
            displayItem.GetComponent<entryData>().lvlID = levels[i].userId.ToString();
            displayItem.GetComponent<entryData>().lvlCreator = levels[i].id.ToString();
            displayItem.GetComponent<entryData>().lvlData = levels[i].levelData;
        }
    }

    public Level rTest()
    {
        return test;
    }


 

}

