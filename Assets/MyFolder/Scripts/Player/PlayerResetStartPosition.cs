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
            transform.position = new Vector3(Random.Range(-1.6f, 1.6f), 1f, Random.Range(2.9f, 6.5f));
        }
    }
}
