using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWinScoreText : MonoBehaviour
{
    public ClockTimeLeft cl;
    private void OnEnable()
    {
        GetComponent<Text>().text = "Score " + cl.winConditionScore + " to Win";
    }
}
