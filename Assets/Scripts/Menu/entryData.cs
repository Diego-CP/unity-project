using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class entryData : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI Creator;
    public string lvlData;
    public string lvlName;
    public string lvlID;
    public string lvlCreator;
    
    private void Start()
    {
        transform.position = Vector3.zero;
        transform.localScale = new Vector3(1,1,1);
        nameText.text = lvlName;
        ID.text = lvlID;
        Creator.text = lvlCreator;
    }
   
    
}
