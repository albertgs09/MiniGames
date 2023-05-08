using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject food;
    public Transform[] foods;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFood), 1, 2);
    }

    void SpawnFood()
    {
        Instantiate(food, foods[RandomNum(foods)].position, foods[RandomNum(foods)].rotation);
        print("Spawned food");
    }
    //spawns an object and sets the player order number
    public void Spawn(GameObject player , int orderNum) 
    {
        GameObject newObj = Instantiate(player, spawnPoints[RandomNum(spawnPoints)].position, spawnPoints[RandomNum(spawnPoints)].rotation);
        newObj.GetComponent<Health>().orderNum = orderNum;
    }
    //returns a random number based on array
    int RandomNum(Transform[] arr)
    {
        return Random.Range(0, arr.Length);
    }
}
