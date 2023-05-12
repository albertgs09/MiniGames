using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer = 30;
    public Text timerText;
    public bool gameStart = false;
    void Update()
    {
        if (gameStart)
        {
            timerText.enabled = true;
            timer -= Time.deltaTime;
            if (timer <= 0) gameStart = false;
        }
        else
        {
            timer = 30;
            timerText.enabled = false;
        }
        timerText.text = ((int)timer).ToString();
    }
}
