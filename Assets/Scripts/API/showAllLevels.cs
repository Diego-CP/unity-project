using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
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

[System.Serializable]
public class leveling
{
    public List<Level> ls;
}
public class showAllLevels : MonoBehaviour
{
    public List<Level> Levels;
    public static Level test;
    private string constring = "https://api-heavent.herokuapp.com/levels";
    bool load;
    public GameObject levelEntryItem;
    public Transform scroll;
    public leveling lsx;

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
                string raw2 = "{ \"ls\":" + raw + "}";
                lsx = JsonUtility.FromJson<leveling>(raw2);            
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
        GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().lvl = lsx.ls;

        if (load)
            dislpayLevels(lsx.ls);
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


}

