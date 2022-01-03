using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunchLadyMovement : MonoBehaviour
{
    public float speed; //sets walking speed for how quickly she moves between A and B
    public float rotationSpeed = 1.0f;//sets rotation speed (should probably be pretty low, >0.5f)
    public float patrolWaitTime = 5;//how long lunch lady stays at A or B before walking to other patrol spot
    public float suspicionWaitTime = 3;
    public Transform pointA;//lunch lady's start patrol spot
    public Transform pointB;//lunch lady's end patrol spot
    public Animator animator;

    //public float minRotPtA = 130;
    //public float maxRotPtA = 180;
    //public float minRotPtB = 158;
    //public float maxRotPtB = 240;

    //public Transform lookAtA;
    //public Transform lookAtB;

    private Transform playerTransform;
    private bool SuspicionState = false;//used to prevent LL from doing unwanted behavior when she is watching the player act suspiciously
    public bool goToB = false;//used for LL to determine what spot she needs to go to next
    public bool goToA = false;//used for LL to determine what spot she needs to go to next

    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
    }

    public void SetSuspision(bool status) { SuspicionState = status; }

    void Update()
    {
        Debug.Log("Suspicious:" + SuspicionState);
        if (!SuspicionState)//if lunch lady is not suspicious she will patrol
        {
            Patrol();
            if (transform.position == pointA.position)//if lunch lady is at point A, she will be told her next spot to go to is point B
            {
                //transform.LookAt(lookAtA);
                rotateView();
                goToB = true;
                goToA = false;
            }
            if (transform.position == pointB.position)//if lunch lady is at point B, she will be told her next spot to go to is point A
            {
                rotateView();
                //transform.LookAt(lookAtB);
                goToA = true;
                goToB = false;
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    void Patrol()
    {
        animator.SetBool("Walking", true);
        Debug.Log("I am patrolling"); 
        if (goToB)
        {
            StartCoroutine(WaitRoutineB());
        }
        if (goToA)
        {
            StartCoroutine(WaitRoutineA());
        }
    }

    void walkToB()
    {
        float step = speed * Time.deltaTime;//creates a non-frame dependent travel speed for LL
        transform.position = Vector3.MoveTowards(playerTransform.position, pointB.position, step);//moves LL towards point B
        transform.LookAt(pointB);//has LL look at point B so she looks where she's walking
    }

    void walkToA()
    {//does the same as walkToB, except for A
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(playerTransform.position, pointA.position, step);
        transform.LookAt(pointA);
    }
    void rotateView()
    {
        //this function is used to make LL rotate while she is standing at point A or B
        float rY;
        animator.SetBool("Walking", false);//stops LL from walking in place

        if (transform.position == pointA.position)//sets the range of rotation for LL at point A
        {
            rY = Mathf.SmoothStep(180, 240, Mathf.PingPong(Time.time * rotationSpeed, 1));//if lunchlady is on point A she will ocilate her rotation between 130 and 180 in the transform. I tried using a varriable but for some reason that didn't work
        }
        else//sets the range of rotation for LL at point B
        {
            rY = Mathf.SmoothStep(130, 180, Mathf.PingPong(Time.time * rotationSpeed, 1));//if lunchlady is on point B she will ocilate her rotation between 180 and 240
        }

        transform.rotation = Quaternion.Euler(0, rY, 0);//returns a rotation around the Y axis
    }
    public void suspicionDetected()
    {
        StartCoroutine(SuspicionRoutine());
        SuspicionState = false;
        GetComponent<LineOfSight>().studentAlert = false;
    }

    //the following two coroutines are used to make a countdown that starts when LL gets to either point A and point B. When countdown is over, she walks to the opposite point

    IEnumerator WaitRoutineA()
    {
        //transform.Rotate(0, -180, 0);
        yield return new WaitForSeconds(patrolWaitTime);
        walkToA();
    }

    IEnumerator WaitRoutineB()
    {
        //transform.Rotate(0, -180, 0);
        yield return new WaitForSeconds(patrolWaitTime);
        walkToB();
    }
    public IEnumerator SuspicionRoutine()
    {
        Debug.Log("Suspicion Routine called");
        StopAllCoroutines();
        yield return new WaitForSeconds(suspicionWaitTime);
        Patrol();
        test();
    }
    public void test()
    {
        Debug.Log("Suspicion Routine Countdown Complete");
    }
}
