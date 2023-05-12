using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveNoNav : MonoBehaviour
{
    private Rigidbody rb;
    private CheckForTargets targets;
    private float speed = 1;
    private Vector3 direction;
    private Transform finishTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targets = GetComponent<CheckForTargets>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = targets.CheckClosestTarget().position - transform.position;
        transform.Translate(direction * speed * Time.deltaTime);
        if(direction.magnitude <= 0)
        {
            targets.CheckClosestTarget();
        }
    }
}
