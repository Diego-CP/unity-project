using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class VictoryCheck : MonoBehaviour
{

    List<GameObject> Bosses = new List<GameObject>();

    public void Update()
    {
        Bosses = GameObject.FindGameObjectsWithTag("Boss").ToList();
    }

    public void DeadBoss(GameObject boss)
    {
        if(Bosses.Contains(boss))
        {
            Bosses.Remove(boss);
        }

        if(Bosses.Count <= 0)
        {
            GameManager.instance.player.Heal(GameManager.instance.player.maxHitpoint);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }
}