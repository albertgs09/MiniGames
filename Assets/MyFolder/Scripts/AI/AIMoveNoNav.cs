using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveNoNav : MonoBehaviour
{
    private Rigidbody rb;
   [SerializeField] private float speed = 1, radius, jumpForce;
    [SerializeField] private LayerMask ground;
    private Vector3 direction;
    private Transform finishTarget, currentTarget;
    [SerializeField] Collider[] targets;
    private float currentDist = 100;
    private int nearestIndx = 0;
    [SerializeField] private Transform eyes;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        finishTarget = GameObject.Find("FinishLine").transform;
        currentTarget = PickaTarget();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        float dist = Vector3.Distance(transform.position, currentTarget.position);
        direction = currentTarget.position - transform.position;

        if (dist >= 1) transform.Translate(direction * speed * Time.deltaTime);
        else currentTarget = PickaTarget();
       

        print(currentTarget.name);
        DetectingWater();

    }

    Transform PickaTarget()
    {
        Array.Clear(targets, 0, targets.Length);
        
        targets = Physics.OverlapSphere(transform.position, radius, ground);
        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(targets[i].transform.position, finishTarget.position);
            if (distance < currentDist)
            {
                currentDist = distance;
                nearestIndx = i;
            }
        }
       
        return targets[nearestIndx].transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Jump()
    {

        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
        print("Jumping");
    }

    void DetectingWater()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(eyes.position, eyes.forward, out hit, 3f))
        {
            Debug.DrawRay(eyes.position, eyes.forward * 1000, Color.red);

            if (IsGrounded())
            {
                if (hit.transform.gameObject.layer == 4)
                {
                    Jump();
                }
            }
            
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, .2f, ground);
    }
}
