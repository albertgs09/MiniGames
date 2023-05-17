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
    private float currentDist = 100;
    private int nearestIndx = 0;
    private GameObject currentLilypad;
    [SerializeField] private Transform eyes;
    [SerializeField]List<Collider> targetsList = new();
    private string name;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        finishTarget = GameObject.Find("FinishLine").transform;
        currentTarget = PickaTarget();
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
            direction = Vector3.zero;
        }

        DetectingWater();

    }

    Transform PickaTarget()
    {
        targetsList.Clear();
        currentDist = 100;
        targetsList = Physics.OverlapSphere(transform.position, radius, ground).ToList();
        print(name);
        if(name != currentLilypad.name)
        { 
            for (int i = 0; i < targetsList.Count; i++)
            {
                float distance = Vector3.Distance(targetsList[i].transform.position, finishTarget.position);
                if (distance < currentDist)
                {
                    currentDist = distance;
                    nearestIndx = i;
                }
            }
            // if(nearestIndx >= targetsList.Count)
            //{
            //    return null;
            //}
            //else
            //{
            //    if (currentLilypad != null && targetsList[nearestIndx].name == currentLilypad.name)
            //    {
            //        if (nearestIndx != targetsList.Count)
            //        {
            //            print("plus");
            //            return targetsList[nearestIndx + 1].transform;
            //        }
            //        else
            //        {
            //            print("random");
            //            return targetsList[UnityEngine.Random.Range(0, targetsList.Count + 1)].transform;
            //        }
            //    }
            //    else
            //    {
            //print("nearest");
            //name = currentLilypad.name;
            //return targetsList[nearestIndx].transform;
            //    }
            //}    
            name = currentLilypad.name;
            return targetsList[nearestIndx].transform;
        }
        else
        {
            for (int i = 0; i < targetsList.Count; i++)
            {
                float distance = Vector3.Distance(targetsList[i].transform.position, finishTarget.position);
                if (targetsList[i].name != name)
                {
                    if (distance < currentDist)
                    {
                        currentDist = distance;
                        nearestIndx = i;
                    }
                }
               
            }
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
    }


 
}
