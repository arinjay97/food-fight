using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int CountDownTime;
    public Text CountdownDisplay;
    public bool isStarted = false;
    public List<GameObject> listToActivate;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

   /* private void OnApplicationPause(bool pause)
    {
        if (isStarted)
        {
            if (isPaused)
            {

            }
        }
    }*/
    IEnumerator CountdownToStart()
    {
        while(CountDownTime > 0)
        {
            GameController.paused = true;
            CountdownDisplay.text = CountDownTime.ToString();
            yield return new WaitForSeconds(1f);
            CountDownTime--;
        }
        CountdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        CountdownDisplay.gameObject.SetActive(false);
        GameController.paused = false;
        BeginGame();
    }

    public void BeginGame()
    {
        foreach(GameObject obj in listToActivate)
        obj.gameObject.SetActive(true);
    }
}
