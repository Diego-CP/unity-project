using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    // Variable to know if the floating text is being used
    public bool active;
    // Have a reference to our own game object
    public GameObject go;
    // Text to be displayed
    public Text txt;
    // Motion of the floating text
    public Vector3 motion;
    // For how long the text will be displayed
    public float duration;
    // At what frame the text was last shown
    public float lastShown;

    public void Show() {
        // Turn on the text
        active = true;
        // Last shown is the current time
        lastShown = Time.time;
        // Set the game object as active
        go.SetActive(active);
    }

    public void Hide() {
        // Deactivate it
        active = false;
        // Change the value of active for the game object
        go.SetActive(active);
    }

    public void UpdateFloatingText() {
        // If the text is not active, don't do anything
        if(!active) 
            return;
        
        // Check if the text has been displayed for the duration established
        if (Time.time - lastShown > duration) 
            // Hide the text
            Hide();

        // Move the object by the motion
        go.transform.position += motion * Time.deltaTime;
        
    }
}
