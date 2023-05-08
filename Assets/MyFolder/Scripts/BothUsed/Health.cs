using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private GameObject blood;
    private Rigidbody rb;
    public UnityEvent OnDeath, OnBegin;
    public GameObject playerObject;
    private PlayerOrder order;
    public int orderNum;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        order = GameObject.Find("GameController").GetComponent<PlayerOrder>();
        if (orderNum == 0) orderNum = order.CheckOrder(gameObject.name);//Gets player order number if they dont have one yet
        OnBegin?.Invoke();
    }
    
    private void Death()
    {
        StopAllCoroutines();
        //add particle Effect   
       // Instantiate(blood, transform.position, transform.rotation);
        rb.constraints = RigidbodyConstraints.None;
        OnDeath?.Invoke();
        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);//forces player up
        StartCoroutine(Respawn());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bar")
        {
            //increases speed of bar
            collision.gameObject.GetComponentInParent<Rotater>().ySpeed += 5;
            Death();
        }
    }

    //Spawns object at a new position
   private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("SpawnPoints").GetComponent<Respawn>().Spawn(playerObject, orderNum);
        Destroy(gameObject);
    }
}
