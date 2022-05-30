using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover {
    // XP Value of defeating this enemy
    public int xpValue = 1;

    // Aggro radius
    public float triggerLength = 0.3f;

    // Distance it will chase the player for
    public float chaseLength = 1.0f;

    // Is it chasing the player?
    private bool chasing;

    // Is it colliding with the player?
    private bool collidingWithPlayer;
    private GameObject spawn;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;

    // This class cannot inherit from Collidable class, so we have to copy the logic
    // Array of things it is colliding with
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start() {
        base.Start();
        // Assign values to each variable
        startingPosition = transform.position;

        // Get the box collider from the first child of the enemy (always Hitbox)
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();

        Invoke("getPlayer", 0);
    }

    private void getPlayer()
    {
        // Assign the position of the player through GameManager
        spawn = GameManager.instance.spawnPoint;
        playerTransform = spawn.transform.GetChild(0).gameObject.transform;
    }

    private void FixedUpdate() {
        // Determine if the player is in chase length
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) {
            // Set the value of chasing depending on if the player is inside aggro range
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength) 
                chasing = true;
            
            if (chasing) {
                // If the enemy is not colliding with the player, move towards him
                if(!collidingWithPlayer) {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            } else {
                // If the enemy is not chasing, return to starting position
                UpdateMotor(startingPosition - transform.position);
            }
        // If the player is not in chase length, return to starting position
        } else {
            // Return to starting position and stop chasing
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlap with Player 
        collidingWithPlayer = false;
        // Look for other colliders inside of this objetcs' collider and place it in hits array
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++) {
            // When nothing is hit, continue to the next frame
            if (hits[i] == null) 
                continue;

            // If something is hit, 
            if (hits[i].tag == "Fighter" && hits[i].name == "Player"){
                collidingWithPlayer = true; 
            }

            // Clear the array
            hits[i] = null;
        }
    }

    protected override void Death() {
        // Destroy this Game Object
        Destroy(gameObject);
        GameManager.instance.experience += xpValue;
        GameManager.instance.ShowText("+ " + xpValue + " XP", 25, Color.green, transform.position, Vector3.up * 20, 1.5f);
    }

}
