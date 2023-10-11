using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Score;
    [SerializeField]
    public GameObject PauseMenu;
    [SerializeField]
    MousePosition mospos;
    bool x;
    int score = 0;
    public static bool scored;
    float lives = 3;
    [SerializeField]
    TextMeshProUGUI live;
    [SerializeField]
    GameObject GameOver;
    private void Start()
    {
        x = true;
        scored=false;
        Score.text = "SCORE:" + score;
        live.text = "LIVES:" + lives;
        GameOver.SetActive(false);
        Time.timeScale = 1;

    }
    private void Update()
    {
        if (lives > 0)
        {
            if (Time.timeScale == 1)
            {
                x = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                x = !x;
                Time.timeScale = Convert.ToInt32(x);
                PauseMenu.SetActive(!x);
            }
        }
        
    }

    public void UpdateScore()
    {
        scored = true;
        score++;
        Score.text = "SCORE:" + score;
    }

    public void UpdateLives()
    {
        lives--;
        live.text = "LIVES:" + lives;
        if (lives == 0)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
    }

 
}
