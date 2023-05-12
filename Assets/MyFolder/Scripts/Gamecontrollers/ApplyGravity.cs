using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    private Rigidbody rb;
    private Finished winTextScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.useGravity = false;
        winTextScript = GameObject.Find("FinishLine").GetComponent<Finished>();
    }

    private void FixedUpdate()
    {
        if (winTextScript.winnerText.enabled)
            rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
            rb.useGravity = true;
    }
}
