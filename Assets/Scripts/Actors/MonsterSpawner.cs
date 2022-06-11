using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public GameObject spawn;
    public Transform playerTransform;
    public float spawnLength = 0.2f;
    private bool detected = false;
    public int quantity;
    private float spawnTime;
    private float spawnWait = 2.5f;
    public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time + spawnWait;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn != null && playerTransform != null)
        {
            if (Vector3.Distance(playerTransform.position, gameObject.transform.position) < spawnLength)
                detected = true;
            if (detected)
            {
                if(quantity > 0)
                {
                    if(spawnTime <= Time.time)
                    {
                        GameObject monster = GameObject.Instantiate(Monster, transform.position, Quaternion.identity) as GameObject;
                        monster.name = Monster.name;
                        monster.transform.parent = gameObject.transform;
                        spawnTime = Time.time + spawnWait;
                        quantity--;
                    }
                }
            }
        }
        else
        {
            getPlayer();
        }
    }



    private void getPlayer()
    {
        // Assign the position of the player through GameManager
        if (GameObject.Find("SpawnPoint") != null)
        {
            spawn = GameManager.instance.spawnPoint;
            playerTransform = spawn.transform.GetChild(0).gameObject.transform;
        }
    }
}
