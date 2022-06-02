using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : Collectable {
    public GameObject projectile;
    protected Quaternion direction;
    protected float cooldown = 0.5f;
    private float lastUse = 1.0f;
    public int faithCost = 1;
    private SpellInventory inventory;

    protected override void Start() {
        base.Start();
        inventory = GameObject.Find("Player").GetComponent<SpellInventory>();
        lastUse = 1.0f;
    }

    protected override void OnCollect() {
        // Add the spell to the inventory
        inventory.AddSpell(gameObject);
        
        // Disable the sprite renderer and the box collider of the object
        GetComponent<Collider2D>().enabled = false;
    }

    public override void Activate() {
        // If more time has passed than the cooldown, the player can use the spell
        if (Time.time - lastUse > cooldown) {
            if (GameManager.instance.player.faith > faithCost) {
                // Reset lastUse as current time
                lastUse = Time.time;

                // Instantiate a projectile accordingly to what direction the player is moving
                float angle = Mathf.Atan2(GameManager.instance.player.currentY, GameManager.instance.player.currentX) * Mathf.Rad2Deg - 90;
                Instantiate(projectile, GameManager.instance.player.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

                // Reduce the player faith by the cost of the spell
                GameManager.instance.player.UseFaith(faithCost);
            } else {
                Debug.Log("Not enough faith.");
            }
        }
    }
}
