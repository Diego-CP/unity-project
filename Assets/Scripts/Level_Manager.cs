using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;

    private void Awake() {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    public Tilemap tilemap;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A)) SaveLevel();
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M)) SaveLevel();
    }   

    void SaveLevel(){
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new LevelData();

        for (int x = bounds.min.x; x < bounds.max.x; x++){
            for(int y = bounds.min.y; y < bounds.max.y; y++){
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                if(temp != null){

                    levelData.tiles.Add(temp);
                    levelData.poses.Add(new Vector3Int(x,y,0));

                }            
            }
        }
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.", json);
    }
    
    void LoadLevel()
    {
        string json = File.ReadAllText(Application.dataPath + "/testLevel.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        tilemap.ClearAllTiles();

        for (int i = 0; i < data.poses.Count; i++ )
        {
            tilemap.SetTile(data.poses[i], data.tiles[i]);
        }
    }


}

public class LevelData{
    public List<TileBase> tiles = new List<TileBase>();
    public List<Vector3Int> poses = new List<Vector3Int>();
}
