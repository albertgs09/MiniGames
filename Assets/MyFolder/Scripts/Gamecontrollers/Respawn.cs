using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public Transform[] spawnPoints;

    //spawns an object and sets the player order number
    public void Spawn(GameObject player , int orderNum) 
    {
        var randNum = Random.Range(0, spawnPoints.Length);
        GameObject newObj = Instantiate(player, spawnPoints[randNum].position, spawnPoints[randNum].rotation);
        newObj.GetComponent<Health>().orderNum = orderNum;
    }
}
