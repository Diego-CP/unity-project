using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCheck : MonoBehaviour
{

    List<GameObject> Bosses = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Bosses.AddRange(GameObject.FindGameObjectsWithTag("Boss"));
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
