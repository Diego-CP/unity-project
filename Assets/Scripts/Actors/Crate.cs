using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter {

    // Override the Death function for when the crate is destroyed
    protected override void Death() {
        Destroy(gameObject);
        // Find the audio manager and play the crate destroy sound with it
        FindObjectOfType<AudioManager>().Play("CrateDestroy");
    }
}
