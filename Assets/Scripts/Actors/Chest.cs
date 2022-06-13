using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {
    // Variable to swap to an empty chest once collected
    public Sprite emptyChest;

    // Variable for the amount of gold to give
    public int goldAmount = 10;
    
    // Override the OnCollide function from Collidable
    protected override void OnCollect()
    {        
        if (!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            // Increase the amount of gold the Player has
            GameManager.instance.gold += goldAmount;
            // Display the amount of gold gained on-screen
            GameManager.instance.ShowText("+ " + goldAmount + " Gold!", 25, Color.yellow, transform.position, Vector3.up * 20, 1.5f);
            // Find the audio manager and play the chest pickup sound with it
            FindObjectOfType<AudioManager>().Play("ChestPickup");
        }
    }
}
