using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public GameObject[] items;
    
    void Start()
    {
        int randNum = Random.Range(0, items.Length);
        GameObject item = Instantiate(items[randNum], transform.position, transform.rotation);
        item.transform.parent = gameObject.transform;    


    }
}
