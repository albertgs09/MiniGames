using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        int randNum = Random.Range(0, items.Length);
        GameObject item = Instantiate(items[randNum], transform.position, transform.rotation);
        item.transform.parent = gameObject.transform;    


    }
}
