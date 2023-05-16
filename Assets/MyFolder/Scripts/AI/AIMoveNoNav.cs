using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject currentLilypad;
    [SerializeField] private Transform eyes;
    List<Collider> targetsList = new();
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
        if(currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.position);
            direction = currentTarget.position - transform.position;

            if (dist >= 1) transform.Translate(direction * speed * Time.deltaTime);
            else {
                currentTarget = PickaTarget();
            } 
        }
       

        if (currentTarget == null) {
            currentTarget = PickaTarget();
        }

        DetectingWater();

    }

    Transform PickaTarget()
    {
        Array.Clear(targets, 0, targets.Length);
        targetsList.Clear();
        currentDist = 100;
        targets = Physics.OverlapSphere(transform.position, radius, ground);
        targetsList = targets.ToList();

        for (int i = 0; i < targetsList.Count; i++)
        {
            print(targetsList[i].name);
            float distance = Vector3.Distance(targetsList[i].transform.position, finishTarget.position);
            if (distance < currentDist)
            {
                currentDist = distance;
                nearestIndx = i;
            }
        }
         if(nearestIndx >= targetsList.Count)
        {
            return null;
        }
        else
        {
            if (targetsList[nearestIndx].name == currentLilypad.name)
            {
                targetsList.RemoveAt(nearestIndx);
                print("Getting new lily");
                if (nearestIndx != targetsList.Count)
                {
                    return targetsList[nearestIndx + 1].transform;
                }
                else
                {
                    return targetsList[nearestIndx].transform;
                }
            }
            else
                return targetsList[nearestIndx].transform;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Jump()
    {

        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
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

    private void OnCollisionEnter(Collision collision)
    {
        currentLilypad = collision.gameObject;
        print(currentLilypad.name);
    }
}
