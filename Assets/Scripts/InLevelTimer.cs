using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InLevelTimer : MonoBehaviour
{
  
    public Text timeCounter;
    private TimeSpan timePlaying;
    private bool timerGoing = false;
    private float elapsedTime; 
  
    public void BeginTimer()
    {
        timerGoing = true;
       // startTime = Time.time;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void endTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing){
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string playTimeStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = playTimeStr;
            yield return null;
        }
    }
}
