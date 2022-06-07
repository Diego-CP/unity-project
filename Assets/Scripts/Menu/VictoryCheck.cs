using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class VictoryCheck : MonoBehaviour
{

    List<GameObject> Bosses = new List<GameObject>();
    public PlayerStats ps;

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
            if(SceneManager.GetActiveScene().name != "Editor")
            {
                GameManager.instance.player.Heal(GameManager.instance.player.maxHitpoint);
                GameObject.Find("Retain").gameObject.GetComponent<RetainOnLoad>().usr.victory++;
                ps.gameObject.GetComponent<PlayerStats>().addData();
                SceneManager.LoadScene("Win");
                
            }
            
        }
    }
   
}