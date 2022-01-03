using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour
{
    private Image suspectMeter;
    public float currentSuspicion;
    public float maxSuspicion = 100.0f;
    LineOfSight susRaiser;


    private void Start()
    {
        suspectMeter = GetComponent<Image>();
        susRaiser = FindObjectOfType<LineOfSight>();
    }

    private void Update()
    {
        currentSuspicion = susRaiser.suspicion;
        suspectMeter.fillAmount = currentSuspicion / maxSuspicion;

        if(susRaiser.canSeePlayer)
        {
            suspectMeter.color = new Color(1, 0, 0, 1);
        }
        if(!susRaiser.canSeePlayer)
        {
            suspectMeter.color = new Color(0, 1, 0, 1);
        }
    }
}
