using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public int[] scores = new int[4] {0,0,0,0};
    public Text[] scoreTexts;
   
   public void UpdateScores(int player, int score)
    {
        scores[player - 1] += score;
        UpdateUI(player-1);
    }


    private void UpdateUI(int player)
    {
        scoreTexts[player].text = "Player" + (player + 1).ToString() +  " " + scores[player].ToString();    
    }
}
