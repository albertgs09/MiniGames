using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private Animator anim;
    private GameObject GameController;
    private Health myHealth;
  

    private void Start()
    {
        GameController = GameObject.Find("GameController");
        myHealth = GetComponentInParent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            //updates score with order number and points
            GameController.GetComponent<PointController>().UpdateScores(myHealth.orderNum, 1);
            Destroy(other.gameObject);
        }
    }
}
