using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrder : MonoBehaviour
{
    public GameObject[] players;

    // Start is called before the first frame update
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Players");
    }

   
    //checks order of players in the beginning of the game by their names
    public int CheckOrder(string name)
    {
        for(int i= 0;i < players.Length;i++)
        {
            if (players[i].name == name)
            {
                return i+1;
            }
        }
        return 0;
    }

}
