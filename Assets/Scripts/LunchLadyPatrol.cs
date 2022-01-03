using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LunchLadyPatrol : MonoBehaviour
{
    public List<Transform> Patrol_Points = new List<Transform>();
    public int start_point_no;
    public float speed;
    public float waitTime = 5;
    

    private Transform playerTransform;
    private Transform TargetLoc;


    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
