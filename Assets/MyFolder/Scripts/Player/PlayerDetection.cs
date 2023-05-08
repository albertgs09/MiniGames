using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetection : MonoBehaviour
{
    private Rigidbody rb;
    public UnityEvent OnBegin, OnDone;
    private GameObject target;
    private GameObject GameController;
    private Health myHealth;
    [SerializeField] private float hitForce = 5, delay = 0.5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        myHealth = GetComponent<Health>();
        GameController = GameObject.Find("GameController");
    }

    private void Hit()
    {
        //Turn off Move and character controller
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector3 direction = (transform.position - target.transform.position).normalized;
        rb.AddForce(direction * hitForce * Time.deltaTime);
        StartCoroutine(Reset());
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "EHurtBox") 
        {
            //Debug.Log("Player Hit");
            target = collision.gameObject;
            Hit();
        }

        if (collision.gameObject.CompareTag("Points"))
        {
            //Adds points
            GameController.GetComponent<PointController>().UpdateScores(myHealth.orderNum, 1);//checks for player order and points to add
            Destroy(collision.gameObject);
        }
    }
    private IEnumerator Reset()
    {
        //Resets velocity and enables movement and character controller
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
    }
}


