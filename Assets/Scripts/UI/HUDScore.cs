using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScore : MonoBehaviour
{

    private Text txtScore;
    private Text txtMultiplier;

    private void Start()
    {
        txtScore = this.FindChildObject("score").GetComponent<Text>();
        txtMultiplier = this.FindChildObject("multiplier").GetComponent<Text>();
    }
    public void UpdateMultiplier(float multiplier)
    {
        txtMultiplier.text = multiplier + "x";
    }
    public void UpdateScore(int addition, int score)
    {
        txtScore.text = score.ToString();
    }
}
