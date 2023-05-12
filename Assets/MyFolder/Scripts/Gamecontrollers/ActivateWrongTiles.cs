using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWrongTiles : MonoBehaviour
{
    private List<GameObject> wrongTiles;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.tag != "RightTiles") 
                child.gameObject.AddComponent<ApplyGravity>();
        }

    }
}
