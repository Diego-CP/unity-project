using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelEditor : MonoBehaviour
{

    [SerializeField] Tilemap defaultTilemap;
    public terrainToggle terrain;
    Tilemap currentTilemap
    {
        //get current tile layer else return default
        get
        {
            if(Level_Manager.instance.layers.TryGetValue((int)Level_Manager.instance.tiles[selectedTileIndex].tilemap, out Tilemap tilemap))
            {
                return tilemap;
                
            }
            else
            {
                return defaultTilemap;
                
            }
            Debug.Log(tilemap);
        }
    }
    //get current tile
    TileBase currentTile
    {
        get
        {
            
            return Level_Manager.instance.tiles[selectedTileIndex].tile;
        }
    }
    
   
   [SerializeField] Camera cam;

    int selectedTileIndex;


    

    //tile placement based on mouse clicks
    private void Update() {
        Vector3Int pos = currentTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));
        

        if(terrain.toggle)
        {
            if (Input.GetMouseButton(0))
            {//pace tiles on current pos
                PlaceTile(pos);
            }

            if (Input.GetMouseButton(1))
            {
                DeleteTile(pos);
            }

            //select tiles with keyboard numpad
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                selectedTileIndex++;
                if (selectedTileIndex >= Level_Manager.instance.tiles.Count)
                    selectedTileIndex = 0;
                Debug.Log(Level_Manager.instance.tiles[selectedTileIndex].name);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedTileIndex--;
                if (selectedTileIndex < 0)
                    selectedTileIndex = Level_Manager.instance.tiles.Count - 1;
                Debug.Log(Level_Manager.instance.tiles[selectedTileIndex].name);
            }
        }

        }
    void PlaceTile(Vector3Int pos)
    {
       currentTilemap.SetTile(pos, currentTile);
    }

    void DeleteTile(Vector3Int pos)
    {
       currentTilemap.SetTile(pos, null);
    }
}
