using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWrongTiles : MonoBehaviour
{
    private List<GameObject> wrongTiles;
    
    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.tag != "RightTiles") 
                child.gameObject.AddComponent<ApplyGravity>();
        }

    }
}
