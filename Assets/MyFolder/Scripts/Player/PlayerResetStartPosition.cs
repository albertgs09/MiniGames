using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetStartPosition : MonoBehaviour
{
    private Transform startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform;     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -50)
        {
            transform.position = new Vector3(0, 0.3f, 0);
        }
    }
}
