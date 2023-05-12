using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveNoNav : MonoBehaviour
{
    private Rigidbody rb;
   [SerializeField] private float speed = 1, radius;
    [SerializeField] private LayerMask masks;
    private Vector3 direction;
    private Transform finishTarget, currentTarget;
    [SerializeField] Collider[] targets;
    private float currentDist = 100;
    private int nearestIndx = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        finishTarget = GameObject.Find("FinishLine").transform;
        currentTarget = PickaTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, currentTarget.position);
        direction = currentTarget.position - transform.position;

        if (dist >= 1) transform.Translate(direction * speed * Time.deltaTime);
        else currentTarget = PickaTarget();


    }

    Transform PickaTarget()
    {
        targets = null;
        
        targets = Physics.OverlapSphere(transform.position, radius, masks);
        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(targets[i].transform.position, finishTarget.position);
            if (distance < currentDist)
            {
                currentDist = distance;
                nearestIndx = i;
            }
        }
        print("Closest lilypad to target is: " + targets[nearestIndx].name);

        return targets[nearestIndx].transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Jump()
    {

    }
}
