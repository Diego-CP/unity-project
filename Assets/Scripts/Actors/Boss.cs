using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    public float[] fireballSpeed = {2.5f, -2.5f};
    public float distance = 0.3f;
    public Transform[] fireballs;

    private void Update() {
        for(int i = 0; i < fireballs.Length; i++) {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }

    protected override void Death()
    {
        base.Death();
        // Find the audio manager and play the boss death sound with it
        FindObjectOfType<AudioManager>().Play("BossDeath");
        GameManager.instance.player.GetComponent<VictoryCheck>().DeadBoss(gameObject);
    }
}