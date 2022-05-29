using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour {
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update() {
        // Look for other colliders inside of this objetcs' and place it in hits array
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++) {
            // When nothing is hit, continue to the next frame
            if (hits[i] == null) 
                continue;

            // If something is hit, call OnCollide
            OnCollide(hits[i]);

            // Clear the array
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll) {
        Debug.Log("OnCollide not implemented in: " + this.name);
    }
}
