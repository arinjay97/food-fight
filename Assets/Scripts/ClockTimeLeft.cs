using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockTimeLeft : MonoBehaviour
{
    public Image timeLeft;
    public Text timeText;
    public int minutes;
    public int sec;
    public bool takingAway = true;
    public ScoreCount scoreCount;
    public int winConditionScore = 100;
    int totalSeconds = 0;
    int TOTAL_SECONDS = 0;
    float fillamount;
    public int winScene;


    void Start()
    {
        timeText.text = minutes + " : " + sec;
        if (minutes > 0)
            totalSeconds += minutes * 60;
        if (sec > 0)
            totalSeconds += sec;
        TOTAL_SECONDS = totalSeconds;
        StartCoroutine(second());
    }

    void Update()
    {
        if (sec == 0 && minutes == 0)
        {
            timeText.text = "Time's Up!";
            StopCoroutine(second());
            if (scoreCount.score >= winConditionScore)
                SceneManager.LoadScene(winScene);
            else
            {
                SceneManager.LoadScene("GameOver");
            }

        }

    }
    IEnumerator second()
    {
        yield return new WaitForSeconds(1f);
        if (sec > 0)
            sec--;
        if (sec == 0 && minutes != 0)
        {
            sec = 60;
            minutes--;
        }

        if(sec <=9)
            timeText.text = minutes + " : 0" + sec;
        else if (sec == 60)
        {
            timeText.text = minutes + 1 + " : 00";
        }
        else
            timeText.text = minutes + " : " + sec;

        fillLoading();
        StartCoroutine(second());
    }

    void fillLoading()
    {
        totalSeconds--;
        float fill = (float)totalSeconds / TOTAL_SECONDS;
        timeLeft.fillAmount = fill;
    }

   
 }

