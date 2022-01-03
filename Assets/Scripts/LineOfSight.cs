using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LineOfSight : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    [Range(0, 100)] public float suspicion;
    public float seeAimingSusRate = 20.0f;
    public float susDecreaseRate = 1.0f;
    private lunchLadyMovement lunchLadyMovement;
    private AudioSource LuchLadyAudiosource;
    public GameObject playerRef;
    public Animator LunchLadyAnimator;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool studentAlert = false;
    public GameObject seenIndicator;

    PlayerActions playerActions;

    private void Start()
    {
        LuchLadyAudiosource = this.GetComponent<AudioSource>();
        lunchLadyMovement = this.GetComponent<lunchLadyMovement>();
        seenIndicator = GameObject.Find("See Player");
        playerActions = FindObjectOfType<PlayerActions>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        suspicion = 0.0f;
    }

    public void StudentAlert(float val)
    {
        Debug.Log("Student Alert");
       // studentAlert = true;
        //transform.LookAt(playerRef.transform);
       // lunchLadyMovement.SetSuspision(true);
       // LunchLadyAnimator.SetBool("Walking", false);
        if (suspicion + val >= 100)
            suspicion = 99f;
        else
            suspicion += val;
       // lunchLadyMovement.suspicionDetected();
    }


    private void Update()
    {
        Debug.Log("Student Alert " + studentAlert);
        if(canSeePlayer && playerActions.inAim && suspicion < 100.0f)
        {
            LunchLadyAnimator.SetBool("Suspisious", true);
            if (!LuchLadyAudiosource.isPlaying) LuchLadyAudiosource.Play();
            lunchLadyMovement.SetSuspision(true);
            suspicion += seeAimingSusRate * Time.deltaTime;
        }
        else if (studentAlert)
        {
            LunchLadyAnimator.SetBool("Walk", false);
            //lunchLadyMovement.suspicionDetected();
        }

        else if(suspicion > 0 && !canSeePlayer)
        {
            lunchLadyMovement.SetSuspision(false);
            LunchLadyAnimator.SetBool("Suspisious", false);
            suspicion -= susDecreaseRate * Time.deltaTime;
        }

        else if(suspicion >= 100)
        {
            LunchLadyAnimator.SetTrigger("GameOver");
            StartCoroutine(ChangeAfterSecondsCoroutine(2));
        }

        else
        {
            LunchLadyAnimator.SetBool("Suspisious", false);
            lunchLadyMovement.SetSuspision(false);
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator ChangeAfterSecondsCoroutine(int sec)
    {
        yield return new WaitForSeconds(sec);
        GameOver();
}

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distranceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distranceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

}
