using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
            rb.useGravity = true;
    }
}
