using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTargets : MonoBehaviour
{
    private Transform target;
    private float lesserDistance;
    private int currentTarget;
    [Range(2f, 20f)]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask masks;
    [SerializeField] Collider[] targets;

  

    public Transform CheckClosestTarget()
    {
        target = null;

        targets = Physics.OverlapSphere(transform.position, radius, masks);
        for (int i = 0; i > targets.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (i == 0) lesserDistance = distance;
            else if (distance < lesserDistance)
            {
                currentTarget = i;
                lesserDistance = distance;
            }
        }
        if (targets.Length > 0) target = targets[currentTarget].transform;
        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
