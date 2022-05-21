using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPortal : Collidable
{
    // Override the OnCollide function
    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }
}
