using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        // When it sollides with the player, call OnCollect
        if (coll.name == "Player")
            OnCollect();
    }

    // Set collected as true when the player touches it
    protected virtual void OnCollect() {
        collected = true;
    }
}
