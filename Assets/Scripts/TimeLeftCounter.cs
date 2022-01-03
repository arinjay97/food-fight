using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeLeftCounter : MonoBehaviour
{
    public GameObject timeLefttxt;
    public ScoreCount scoreCount;
    public int winConditionScore = 30;
    public int TimeLeftinSec = 120;
    public bool takingAway = true;
   

    private void Awake()
    {
        takingAway = false;
    }

    public void Update()
    {
        if(takingAway == false && TimeLeftinSec > 0)
        {
            StartCoroutine(TimeLeft());
        }
        else if (takingAway == false && TimeLeftinSec == 0)
        {
            if (scoreCount.score >= winConditionScore) { }
            //  SceneManager.LoadScene("Win");
            else { }
               // SceneManager.LoadScene("GameOver");
        }
        

    }

    IEnumerator TimeLeft()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        TimeLeftinSec -= 1;
        timeLefttxt.GetComponent<Text>().text = "Time Left:" + TimeLeftinSec;
        takingAway = false;
    }
}
