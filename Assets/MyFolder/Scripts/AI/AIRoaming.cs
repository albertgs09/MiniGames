using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRoaming : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] private float range;
    [SerializeField] private Transform centrePoint;
    private CheckForTargets targetting;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("IsWalking", false);
        targetting = GetComponent<CheckForTargets>();
    }

    void Update()
    {

        if(agent != null)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //Checks closest target and sets the target to the destination if found
                if (targetting.CheckClosestTarget() != null)
                {
                    agent.SetDestination(targetting.CheckClosestTarget().position);
                    Debug.DrawRay(targetting.CheckClosestTarget().position, Vector3.up, Color.green, 2.0f);
                }
                else
                {//picks a random point within a radius
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        agent.SetDestination(point);
                        
                    }
                }
            }
            if (agent.isStopped)
                anim.SetBool("IsWalking", false);
            else
                anim.SetBool("IsWalking", true);
        }
      
    }

    void StopOrNot()
    {
        if (agent.enabled)
            agent.isStopped = true;
        StartCoroutine(WaitForNextPoint());
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    private IEnumerator WaitForNextPoint()
    {

        yield return new WaitForSeconds(1.5f);
        if(agent.enabled)
            agent.isStopped = false;


    }
}
