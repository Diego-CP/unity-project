using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AoeSpell : Collectable {
    protected float spellRange = 0.5f;
    public int spellDamage = 1;
    public float spellPushForce = 5.0f;
    protected float cooldown = 0.5f;
    public float lastUse = 1.0f;
    public int faithCost = 2;
    public SpellInventory inventory;

    protected override void Start() {
        base.Start();
        lastUse = 1.0f;
        getPlayer();
    }
    private void Update()
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
            if(GameObject.Find("Player") != null)
                inventory = GameManager.instance.player.gameObject.GetComponent<SpellInventory>();

            
        
    }

    protected override void OnCollect() {
       
            inventory.AddSpell(gameObject); // Add the spell to the inventory
            GetComponent<Collider2D>().enabled = false;
        
            

        // Disable the sprite renderer and the box collider of the object
        
    }

    public override void Activate() {
        
            // If more time has passed than the cooldown, the player can use the spell
            if (Time.time - lastUse > cooldown)
            {
                if (GameManager.instance.player.faith > faithCost)
                {
                    // Reset lastUse as current time
                    lastUse = Time.time;

                    // List of colliders that oerlap with the spell's circular hitbox
                    Collider2D[] collidedBoxes = Physics2D.OverlapCircleAll(GameManager.instance.player.transform.position, spellRange);
                    // When nothing is hit, continue to the next frame
                    for (int i = 0; i < collidedBoxes.Length; i++)
                    {
                        // When nothing is hit, continue
                        if (collidedBoxes[i] == null)
                            continue;

                        // Check if what is being collided with is a Fighter and is not the player
                        if (collidedBoxes[i].tag == "Fighter" || collidedBoxes[i].tag == "Boss")
                        {
                            if (collidedBoxes[i].name != "Player")
                            {
                                // Create a Damage object
                                Damage dmg = new Damage
                                {
                                    damageAmount = spellDamage,
                                    origin = GameManager.instance.player.transform.position,
                                    pushForce = spellPushForce
                                };

                                collidedBoxes[i].SendMessage("RecieveDamage", dmg);
                            }
                        }
                    }

                    // Reduce the player faith by the cost of the spell
                    GameManager.instance.player.UseFaith(faithCost);
                }
                else
                {
                    Debug.Log("Not enough faith.");
                }
            }
        
  
    }

}
