using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunchLady : MonoBehaviour
{

    public float walkSpeed = 0.2f;
    public float walkTime = 3.0f;
    public float stopTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        walkRight();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void walkRight()
    {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);
            //StartCoroutine(stopWalkRoutine());
            Debug.Log("Walking right");       
     }


    void walkLeft()
    {
            transform.Translate(0, 0, -walkSpeed * Time.deltaTime);
            Debug.Log("Walking left");
    }

    void stopWalk()
    {
        StartCoroutine(starttWalkRoutine());
        transform.Translate(0, 0, 0 * Time.deltaTime);
        Debug.Log("Trying to stop");
        
    }

    IEnumerator stopWalkRoutine()
    {
        yield return new WaitForSeconds(walkTime);
        stopWalk();

    }

    IEnumerator starttWalkRoutine()
    {
        yield return new WaitForSeconds(stopTime);
        walkLeft();

    }

}
