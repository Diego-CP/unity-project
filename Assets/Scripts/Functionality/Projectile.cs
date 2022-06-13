using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable {
    public float speed = 2.0f;
    public float lifeTime = 1.5f;
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    
    protected override void Start() {
        base.Start();
        Invoke("DestroyProjectile", lifeTime);
    }

    public override void Update() {
        base.Update();
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    protected override void OnCollide(Collider2D coll) {
        // Check if what is being collided with is a Fighter and is not the player
        if (coll.tag == "Fighter" || coll.tag == "Boss") {
            if (coll.name != "Player") {
                // Create a Damage object
                Damage dmg = new Damage{
                    damageAmount = damagePoint,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("RecieveDamage", dmg);
                DestroyProjectile();
            }
        }
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }
}
