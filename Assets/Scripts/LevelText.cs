using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : GameManager {
    private TextMeshProUGUI textMesh;

    void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void AddExperience() {
        textMesh.text = "Lv. " + GameManager.instance.level;
    }
}