using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
//Needs Rigidbody for affecting physics, with isKinematics and gravity enabled
//Freeze Rotation X and Z
public class Jump : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 jumpDir;
    public UnityEvent OnBegin, OnDone;
    [SerializeField] private float jumpSpeed, jumpH,delay;
    [SerializeField] private LayerMask ground;
    void Start()
    {
        jumpDir = new Vector3(0, jumpH, 0);
        rb = GetComponent<Rigidbody>();
    }

   
    void Jumping()
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        rb.isKinematic = false;
        rb.AddForce(jumpDir * jumpSpeed, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bar") Jumping();
    }


    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;
        OnDone?.Invoke();
    }
}
