using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable {
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll) {
        // Check if the hitbox is colliding with the player
        if(coll.tag == "Fighter" && coll.name == "Player") {
            // Create a new Damage object and send it to the player
            Damage dmg = new Damage{
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
    }
}
