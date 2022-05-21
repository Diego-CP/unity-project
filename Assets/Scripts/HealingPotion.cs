using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : Collectable {
    public int hpAmount = 5;
    protected override void OnCollect() {
        // Remove the game object
        Destroy(gameObject);
        // Call Heal to heal the Player by hpAmount
        GameManager.instance.player.Heal(hpAmount);
        // Display the amount of HP gained on-screen
        GameManager.instance.ShowText("+ " + hpAmount + " HP", 25, Color.red, transform.position, Vector3.up * 20, 1.5f);
    }
}
