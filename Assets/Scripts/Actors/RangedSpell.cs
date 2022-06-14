using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : Collectable {
    public GameObject projectile;
    protected Quaternion direction;
    protected float cooldown = 0.5f;
    private float lastUse = 1.0f;
    public int faithCost = 1;
    public SpellInventory inventory;

    protected override void Start() {
        base.Start();
        lastUse = 1.0f;
        getPlayer();
        
            
    }
    public override void Update()
    {
        base.Update();
        if(inventory == null)
        {
            getPlayer();
        }
    }

    private void getPlayer()
    {
        // Assign the position of the player through GameManager
            if(GameObject.Find("Player") != null && GameObject.Find("SpawnPoint"))
                inventory = GameObject.Find("Player").gameObject.GetComponent<SpellInventory>();

            
        
    }

    protected override void OnCollect() {

        if (GameObject.Find("SpawnPoint") != null)
        {
            // Add the spell to the inventory
            inventory.AddSpell(gameObject);

            // Disable the sprite renderer and the box collider of the object
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void Activate() {
        if (GameObject.Find("SpawnPoint") != null)
        {
            // If more time has passed than the cooldown, the player can use the spell
            if (Time.time - lastUse > cooldown)
            {
                if (GameManager.instance.player.faith > faithCost)
                {
                    // Reset lastUse as current time
                    lastUse = Time.time;

                    // Instantiate a projectile accordingly to what direction the player is moving
                    float angle = Mathf.Atan2(GameManager.instance.player.currentY, GameManager.instance.player.currentX) * Mathf.Rad2Deg - 90;
                    Instantiate(projectile, GameManager.instance.player.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

                    // Reduce the player faith by the cost of the spell
                    GameManager.instance.player.UseFaith(faithCost);

                    // Find the audio manager and play the ranged spell sound with it
                    FindObjectOfType<AudioManager>().Play("RangedSpellSound");
                }
                else
                {
                    Debug.Log("Not enough faith.");
                }
            }


        }

    }
}
