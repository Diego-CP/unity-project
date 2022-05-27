using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Customtile", menuName = "LevelEditor/Tile")]//custom scripted tiles for automatic layer positioning
public class CustomTile : ScriptableObject
{
    public TileBase tile;
    public string id;
    public Level_Manager.Tilemaps tilemap;
}