using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject pl = GameObject.Instantiate(Player, transform.position, Quaternion.identity) as GameObject;
        pl.name = "Player";
        pl.transform.parent = gameObject.transform;
    }
}
