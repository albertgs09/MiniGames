using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finished : MonoBehaviour
{
    public Timer timer;
    public Text winnerText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Players"))
        {
            timer.gameStart = false;
            winnerText.enabled = true ;
            winnerText.text = collision.gameObject.name + " Wins!";
        }
    }
}
