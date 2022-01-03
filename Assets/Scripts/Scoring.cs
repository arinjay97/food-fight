using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int score = 1;
    ScoreCount sc;
    // Start is called before the first frame update
    void Start()
    {
        sc = FindObjectOfType<ScoreCount>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addScore(int s)
    {
        sc.score += s;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "food")
        {
           // collision.gameObject.tag = "Untagged";
            sc.score = sc.score + score;
        }
    }
}
