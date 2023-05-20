using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttacking : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Players"))
        {
            print("Player Near");
            anim.SetTrigger("Attack");
        }
    }
}
