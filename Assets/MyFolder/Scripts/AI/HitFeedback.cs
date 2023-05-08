using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitFeedback : MonoBehaviour
{
    private Rigidbody rb;
    private bool canHit;
    private GameObject target;
    public UnityEvent OnBegin, OnDone;
    [SerializeField] private float hitForce = 5, delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if(canHit && Input.GetButtonDown("Fire1"))
        {
            //turn off AI roaming script, jump and nav mesh agent

            StopAllCoroutines();
            OnBegin?.Invoke();
            rb.isKinematic = false;
            Vector3 direction = (transform.position - target.transform.position).normalized;
            rb.AddForce(direction * hitForce);
            StartCoroutine(Reset());
            canHit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HurtBox")
        {
            target = other.gameObject;
            canHit = true;
        }
    } 
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "HurtBox") canHit = false;
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        OnDone?.Invoke();
    }
}
