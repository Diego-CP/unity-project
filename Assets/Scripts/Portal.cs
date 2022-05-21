using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    // Array with all the scene names
    public string[] sceneNames;
    public int currentScene = 1;

    // Override the OnCollide function
    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            // Save the current game state before changing scene
            GameManager.instance.SaveState();
            // Teleport the player to a random scene
            string sceneName = sceneNames[currentScene];
            currentScene += 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
