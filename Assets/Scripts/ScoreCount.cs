using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreCount : MonoBehaviour
{

    private int m_score = 0;
    public Text scoreText;

    public int score
    {
        get
        {
            return m_score;
        }

        set
        {
            m_score = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = m_score.ToString();
    }
}
