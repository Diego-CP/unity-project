using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;
    public ItemController[] itemButtons;
    public GameObject[] itemPrefabs;
    public int currentButton;
    public string saveAssetTag1 = "Boss";
    public string saveAssetTag2 = "Fighter";
    public string saveAssetTag3 = "Spawn";
    public string saveAssetTag4 = "Game";
    private GameObject[] assetsToSave;
    [SerializeField] private string[] assetNames;
    [SerializeField] private Vector3[] assetPositions;
    public GameObject[] possibleObjects;
    public GameObject SpawnPoint;
    private bool flag = false;
    public List<CustomTile> tiles = new List<CustomTile>();
    [SerializeField] List<Tilemap> tilemaps = new List<Tilemap>();
    public Dictionary<int, Tilemap> layers = new Dictionary<int, Tilemap>();
    public string level;

    // Retrieve the name of this scene.
    string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        layers.Clear();
        if (instance == null) instance = this;
        else Destroy(this);//clear lvl on start

        foreach(Tilemap tilemap in tilemaps)
        {
            foreach(Tilemaps num in System.Enum.GetValues(typeof(Tilemaps)))
            {
                if (tilemap.name == num.ToString())
                {
                    if (!layers.ContainsKey((int)num)) layers.Add((int)num, tilemap);//get all tilemaps
                }
            }
        }
    }

    private void Start()
    {
        if (sceneName == "LevelLoad")
        {
            LoadLevel(GameObject.Find("Retain").GetComponent<RetainOnLoad>().currentLvlData, false);
        }
    }
    public enum Tilemaps
    {
        Floor = 0,
        Design = 1,
        Collision = 5
    }

    private void Update()
    {
        if (sceneName == "Editor")
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (Input.GetMouseButtonDown(0) && itemButtons[currentButton].clicked)
            {
                itemButtons[currentButton].clicked = false;
                Instantiate(itemPrefabs[currentButton], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A)) 
                Savelevel();
            
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M)) 
                LoadLevel("", true);
        }
    }

    public void Savelevel()
    {
        LevelData levelData = new LevelData();
        
        GameObject[] assets1 = GameObject.FindGameObjectsWithTag(saveAssetTag1); 
        GameObject[] assets2 = GameObject.FindGameObjectsWithTag(saveAssetTag2);
        GameObject[] assets3 = GameObject.FindGameObjectsWithTag(saveAssetTag3);
        //GameObject[] assets3 = GameObject.FindGameObjectsWithTag("MainCamera");
        assetsToSave = assets1.Concat(assets2).ToArray().Concat(assets3).ToArray();

        foreach (var item in assetsToSave)
        {
            levelData.items.Add(new itemData(item.name));
        }

        for (int j = 0; j < assetsToSave.Length; j++)
        {
            levelData.items[j].assetPosition = assetsToSave[j].transform.position;
        }

        foreach (var item in layers.Keys)//add layers to layer data
        {
            levelData.layers.Add(new LayerData(item));
        }

        foreach (var layerData in levelData.layers)
        {
            if (!layers.TryGetValue(layerData.layer_id, out Tilemap tilemap)) break;

            //check bounds
            BoundsInt bounds = tilemap.cellBounds;
            
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                
                    CustomTile temptile = tiles.Find(t => t.tile == temp);

                    if (temptile != null)
                    {
                        layerData.tiles.Add(temptile.id);
                        layerData.poses_x.Add(x);
                        layerData.poses_y.Add(y);
                    }
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        level = json;
        Debug.Log(level);
        //File.WriteAllText(Application.dataPath + "/testLevel.json", json);

        //debug
        Debug.Log("Level was saved");
    }

    public string GetJson()
    {
        return level;
    }

    public void LoadLevel(string dlFile, bool def = true)
    {
        LevelData levelData;
        if (def)
        {
            string json;
            json = File.ReadAllText(Application.dataPath + "/testLevel.json");
            levelData = JsonUtility.FromJson<LevelData>(json);
        }
        else
        {
            levelData = JsonUtility.FromJson<LevelData>(dlFile.Replace((char)39,(char)34));
        }

        foreach (var data in levelData.layers)
        {
            if (!layers.TryGetValue(data.layer_id, out Tilemap tilemap)) break;
            tilemap.ClearAllTiles();
            
            for (int i = 0; i < data.tiles.Count; i++)
            {
                
                tilemap.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.name == data.tiles[i]).tile);
            }
        }

        foreach(var item in levelData.items)
        {
            if (item.assetName.Contains("SpawnPoint"))
            {
                flag = true;
                Instantiate(SpawnPoint, item.assetPosition, Quaternion.identity);
            }
        }

        if (!flag)
        {
            Instantiate(SpawnPoint, new Vector3Int(0, 0, 0), Quaternion.identity);
        }

        item_creation(levelData);

        Debug.Log("Level was loaded");
    }

    void item_creation(LevelData levelData)
    {
        foreach (var item in levelData.items)
        {
            for (int i = 0; i < possibleObjects.Length; i++)
            {
                if (item.assetName.Contains(possibleObjects[i].name))
                {
                    Instantiate(possibleObjects[i], item.assetPosition, Quaternion.identity);
                }
            }
        }
    }
    
}

[System.Serializable]
public class LevelData
{
    public List<LayerData> layers = new List<LayerData>();
    public List<itemData> items = new List<itemData>();
}

[System.Serializable]
public class LayerData
{
    public int layer_id;
    public List<string> tiles = new List<string>();
    public List<int> poses_x = new List<int>();
    public List<int> poses_y = new List<int>();

    public LayerData(int id)
    {
        layer_id = id;
    }
}

[System.Serializable]
public class itemData
{
    public string assetName;
    public Vector3 assetPosition;
    public itemData(string id)
    {
        assetName = id;
    }
}