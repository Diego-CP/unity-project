using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Amount of damage done to the enemy
    public int damagePoint = 1;

    // Amount of knockback force
    public float pushForce = 2.0f;

    // Level of the weapon
    public int weaponLevel = 0;
    // Have a sprite renderer to change the sprite of the weapon with level
    private SpriteRenderer spriteRenderer;

    // Cooldown time for swings
    private float cooldown = 0.5f;

    // Timestamp of last swing
    private float lastSwing;

    private Animator anim;


    protected override void Start() {
        base.Start();
        // Get the sprite renderer component to change the sprites
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        // Check if the player can swing (using spacebar)
        if(Input.GetKeyDown(KeyCode.U)) {
            // If more time has passed than the cooldown, the player can swing
            if (Time.time - lastSwing > cooldown) {
                // Reset lastSwing as current time
                lastSwing = Time.time;
                anim.SetTrigger("Swing");

            }
        }
    }

    protected override void OnCollide(Collider2D coll) {
        // Check if what is being collided with is a Fighter or Boss and is not the player
        if (coll.tag == "Fighter" || coll.tag == "Boss") {
            if (coll.name != "Player") {
                // Create a Damage object
                Damage dmg = new Damage{
                    damageAmount = damagePoint,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("RecieveDamage", dmg);
            }
        }
        
    }

}
