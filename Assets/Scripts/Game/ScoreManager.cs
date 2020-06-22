using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private bool gameOver;
    private List<float> multiplierLastsUntil;

    public static ScoreManager main;

    void Awake()
    {
        main = this;
    }

    private void Start()
    {
        score = 0;
        gameOver = false;
        multiplierLastsUntil = new List<float>();
    }

    public void AddMultiplier(float time)
    {
        multiplierLastsUntil.Add(Time.time + time);
        UIManager.main.UpdateMultiplier(GetMultiplier());
    }

    public void AddScore(int addition)
    {
        if(!gameOver)
        {
            this.score += addition * GetMultiplier();
            UIManager.main.UpdateScore(addition, score);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMultiplier()
    {
        return multiplierLastsUntil.Count + 1;
    }

    private void Update()
    {
        if(multiplierLastsUntil.Count > 0 && Time.time > multiplierLastsUntil[0])
        {
            multiplierLastsUntil.RemoveAt(0);
            UIManager.main.UpdateMultiplier(GetMultiplier());
        }
    }
}
